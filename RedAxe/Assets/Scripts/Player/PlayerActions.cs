using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerActions : MonoBehaviour
{
    private Camera _camera;
    private GameObject _car;
    private GameObject _movement;
    private FirstPersonMovement _firstPersonMovement;
    private RCC_CarControllerV3 _rccCarControllerV3;
    private CarAttributes _currentTradeCarAttributes;
    private PlayerInventory _playerInventory;
    private Npc _currentNpc;
    private Bargain _bargain;
    
    [Space(30)]
    public TMP_Text carNameText;
    public TMP_Text carModelYearText;
    public TMP_Text carKilometerText;
    public TMP_Text carGearText;
    public TMP_Text carFuelText;
    public TMP_Text carPriceText;
    [Space(30)]
    public Image carColorImage;
    public Image carBodyDamageImage;
    public Image carFrontDamageImage;
    public Image carRearDamageImage;
    public Image carLeftDamageImage;
    public Image carRightDamageImage;
    [Space(30)]
    public TMP_Text carBodyDamageText;
    public TMP_Text carFrontDamageText;
    public TMP_Text carRearDamageText;
    public TMP_Text carLeftDamageText;
    public TMP_Text carRightDamageText;
    [Space(30)]
    public Image originalCarColorImage;
    public Image paintedCarColorImage;
    public Image damagedCarColorImage;
    [Space(30)]
    public TMP_InputField bargainInputField;
    [Space(30)]
    public GameObject carTradeUI;
    public GameObject carSellUI;
    public GameObject carBuyUI;
    [Space(30)]
    public GameObject interactionUI;
    public Image cursor;
    public GameObject cursorUI;
    [Space(30)]
    public RCC_Camera rccCamera;
    private Transform _cameraTransform;
    public GameObject rccCameraObject;
    public LayerMask targetLayerMask;
    [Space(30)]
    public ChatBox chatBox;
    public GameObject computerCanvas;
    public DynamicContentHeight ComputerContent;
    public ComputerActions computerActions;

    private void Start()
    {
        _cameraTransform = UnityEngine.Camera.main.transform;
        _bargain = GetComponent<Bargain>();
        _camera = Camera.main;
        _movement = transform.GetChild(0).gameObject;
        _firstPersonMovement = _movement.GetComponent<FirstPersonMovement>();
        _playerInventory = GetComponent<PlayerInventory>();
        StartCoroutine(Player());
    }

    IEnumerator Player()
    {
        yield return null;
        while (true)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out var hit, 1, targetLayerMask))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    interactionUI.SetActive(true);
                    _car = hit.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.E) && _car.GetComponent<CarAttributes>().isPlayerBought)
                    {
                        _rccCarControllerV3 = _car.GetComponent<RCC_CarControllerV3>();
                        GetInCar();
                        interactionUI.SetActive(false);
                        yield break;
                    }
                }
                
                else if (hit.collider.CompareTag("NPC"))
                {
                    interactionUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<Npc>().StartInteraction();
                        interactionUI.SetActive(false);
                        yield break;
                    }
                }
                
                else if (hit.collider.CompareTag("Computer"))
                {
                    interactionUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        computerCanvas.SetActive(true);
                        interactionUI.SetActive(false);
                        DisablePlayer();
                        StartCoroutine(ComputerCamera());
                        yield break;
                    }
                }
                
                else
                {
                    interactionUI.SetActive(false);
                }
            }
            
            else
            {
                interactionUI.SetActive(false);
            }
            
            yield return null;
        }
    }

    IEnumerator Car()
    {
        yield return null;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetOutCar();
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator ComputerCamera()
    {
        var cameraOldPosition = _cameraTransform.localPosition;
        var cameraOldRotation = _cameraTransform.localRotation;
        var focus = computerCanvas.transform.parent.GetChild(1);
        
        yield return null;
        while (true)
        {
            if (_cameraTransform != null)
            {
                _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, focus.position, Time.deltaTime * 8f);
                _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, focus.rotation, Time.deltaTime * 8f);
                
                if (Vector3.Distance(_cameraTransform.position, computerCanvas.transform.parent.GetChild(1).position) < 0.004f)
                {
                    _cameraTransform.position = focus.position;
                    _cameraTransform.rotation = focus.rotation;
                    ComputerContent.UpdateContentHeight();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _cameraTransform.position = focus.position;
                _cameraTransform.rotation = focus.rotation;
                StartCoroutine(PlayerCamera(cameraOldPosition, cameraOldRotation));
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator PlayerCamera(Vector3 cameraOldPosition, Quaternion cameraOldRotation)
    {
        yield return null;
        while (true)
        {
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, cameraOldPosition, Time.deltaTime * 8f);
            _cameraTransform.localRotation = Quaternion.Lerp(_cameraTransform.localRotation, cameraOldRotation, Time.deltaTime * 8f);
            
            if (Vector3.Distance(_cameraTransform.localPosition, cameraOldPosition) < 0.004f)
            {
                _cameraTransform.localPosition = cameraOldPosition;
                _cameraTransform.localRotation = cameraOldRotation;
                
                computerCanvas.SetActive(false);
                EnablePlayer(false);
                
                yield break;
            }

            yield return null;
        }
    }

    private void GetInCar()
    {
        if (_car)
        {
            cursor.enabled = false;

            _rccCarControllerV3.enabled = true;
            rccCameraObject.SetActive(true);
            rccCamera.cameraTarget.playerVehicle = _rccCarControllerV3;

            _movement.SetActive(false);
            StartCoroutine(Car());
        }
    }

    private void GetOutCar()
    {
        if (_car)
        {
            cursor.enabled = true;

            rccCameraObject.SetActive(false);
            _rccCarControllerV3.enabled = false;

            _movement.SetActive(true);
            _movement.transform.position = _car.transform.position + new Vector3(2, 0, 2);
            StartCoroutine(Player());
        }
    }

    public void DisablePlayer(bool trade = false, CarAttributes carAttributes = null, GameObject NPC = null,
        Npc.NpcType npcType = Npc.NpcType.seller)
    {
        _firstPersonMovement.enabled = false;
        
        cursorUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (trade)
        {
            carTradeUI.SetActive(true);
            _currentTradeCarAttributes = carAttributes;
            _currentNpc = NPC.GetComponent<Npc>();
            ShowCarAttributesUI();
            if (npcType == Npc.NpcType.seller)
            {
                carSellUI.SetActive(false);
                carBuyUI.SetActive(true);
                _bargain.StartBargain(true, _currentTradeCarAttributes.salePrice, _currentNpc);
            }
            else if (npcType == Npc.NpcType.buyer)
            {
                carSellUI.SetActive(true);
                carBuyUI.SetActive(false);
                _bargain.StartBargain(false, _currentTradeCarAttributes.salePrice, _currentNpc);
            }
        }
    }

    public void EnablePlayer(bool trade)
    {
        cursorUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _firstPersonMovement.enabled = true;
        StartCoroutine(Player());
        if (trade)
        {
            carTradeUI.SetActive(false);
        }
    }

    private void ShowCarAttributesUI()
    {
        carNameText.text = _currentTradeCarAttributes.carModelName;
        carModelYearText.text = _currentTradeCarAttributes.carModelYear.ToString();
        carKilometerText.text = _currentTradeCarAttributes.carKilometer.ToString();
        carFuelText.text = _currentTradeCarAttributes.carFuelType == 0 ? "Gasoline" : "Diesel";
        carGearText.text = _currentTradeCarAttributes.carGearType == 0 ? "Manual" : "Automatic";
        carPriceText.text = _currentTradeCarAttributes.salePrice.ToString();
        carColorImage.color = _currentTradeCarAttributes.carColor;

        carBodyDamageImage.color = _currentTradeCarAttributes.bodyDamagePercentage > 0 ? damagedCarColorImage.color :
            _currentTradeCarAttributes.isBodyPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carFrontDamageImage.color = _currentTradeCarAttributes.frontDamagePercentage > 0 ? damagedCarColorImage.color :
            _currentTradeCarAttributes.isFrontPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carRearDamageImage.color = _currentTradeCarAttributes.rearDamagePercentage > 0 ? damagedCarColorImage.color :
            _currentTradeCarAttributes.isRearPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carLeftDamageImage.color = _currentTradeCarAttributes.leftDamagePercentage > 0 ? damagedCarColorImage.color :
            _currentTradeCarAttributes.isLeftPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carRightDamageImage.color = _currentTradeCarAttributes.rightDamagePercentage > 0 ? damagedCarColorImage.color :
            _currentTradeCarAttributes.isRightPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;

        carBodyDamageText.text = _currentTradeCarAttributes.bodyDamagePercentage == 0
            ? "Body"
            : "Body " + _currentTradeCarAttributes.bodyDamagePercentage + "%";
        carFrontDamageText.text = _currentTradeCarAttributes.frontDamagePercentage == 0
            ? "Front"
            : "Front " + _currentTradeCarAttributes.frontDamagePercentage + "%";
        carRearDamageText.text = _currentTradeCarAttributes.rearDamagePercentage == 0
            ? "Rear"
            : "Rear " + _currentTradeCarAttributes.rearDamagePercentage + "%";
        carLeftDamageText.text = _currentTradeCarAttributes.leftDamagePercentage == 0
            ? "Left"
            : "Left " + _currentTradeCarAttributes.leftDamagePercentage + "%";
        carRightDamageText.text = _currentTradeCarAttributes.rightDamagePercentage == 0
            ? "Right"
            : "Right " + _currentTradeCarAttributes.rightDamagePercentage + "%";
    }

    public void BuyCar()
    {
        if (!_currentTradeCarAttributes) return;
        
        if (_currentNpc.angryNotGonnaSell)
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry),
                false);
            return;
        }

        if (_currentTradeCarAttributes.bargainPrice <= PlayerPrefs.GetInt("Money"))
        {
            _playerInventory.SubtractMoney(_currentTradeCarAttributes.bargainPrice);
            _currentTradeCarAttributes.isPlayerBought = true;
            PlayerCarSaver.SaveCarAttributes(_currentTradeCarAttributes);
            Destroy(_currentNpc.gameObject);
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Accept),
                true);
        }
        else
        {
            chatBox.AddMessage("I don't have enough money. :(", true);
        }
    }

    public void SellCar()
    {
        if (!_currentTradeCarAttributes) return;
        _playerInventory.AddMoney(_currentTradeCarAttributes.bargainPrice);
        PlayerCarDeleter.DeleteCarAttributes(_currentTradeCarAttributes.carKey);
        computerActions.RemoveCarFromSellingBox(_currentTradeCarAttributes.carKey);
        Destroy(_currentTradeCarAttributes.gameObject);
        Destroy(_currentNpc.gameObject);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Accept), false);
    }

    public void PlayerBargainRequest()
    {
        bool isParsed = int.TryParse(bargainInputField.text, out var parsedPrice);
        if(isParsed) { _bargain.OfferBargain(true, parsedPrice); }
    }
}