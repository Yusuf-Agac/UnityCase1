using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerActions : MonoBehaviour
{
    private Camera cam;
    private GameObject car;
    private GameObject movement;
    private FirstPersonMovement firstPersonMovement;
    private RCC_CarControllerV3 rccCarControllerV3;
    private CarAttributes currentTradeCarAttributes;
    private PlayerInventory playerInventory;
    private GameObject currentNPC;
    
    public TMP_Text carNameText;
    public TMP_Text carModelYearText;
    public TMP_Text carKilometerText;
    public TMP_Text carGearText;
    public TMP_Text carFuelText;
    public TMP_Text carPriceText;
    public Image carColorImage;
    public Image carBodyDamageImage;
    public Image carFrontDamageImage;
    public Image carRearDamageImage;
    public Image carLeftDamageImage;
    public Image carRightDamageImage;
    public TMP_Text carBodyDamageText;
    public TMP_Text carFrontDamageText;
    public TMP_Text carRearDamageText;
    public TMP_Text carLeftDamageText;
    public TMP_Text carRightDamageText;
    public Image originalCarColorImage;
    public Image paintedCarColorImage;
    public Image damagedCarColorImage;
    public GameObject carTradeUI;
    public GameObject interactionUI;
    public Image cursor;
    public LayerMask targetLayerMask;
    public RCC_Camera rccCamera;
    public GameObject rccCameraObject;

    private void Start()
    {
        cam = Camera.main;
        movement = transform.GetChild(0).gameObject;
        firstPersonMovement = movement.GetComponent<FirstPersonMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        StartCoroutine(Player());
    }

    IEnumerator Player()
    {
        yield return null;
        while (true)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = cam.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out var hit, 1, targetLayerMask))
            {
                if (hit.collider.CompareTag("Car"))   
                {
                    interactionUI.SetActive(true);
                    car = hit.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.E) && car.GetComponent<CarAttributes>().isPlayerBought)
                    {
                        rccCarControllerV3 = car.GetComponent<RCC_CarControllerV3>();
                        GetInTheCar();
                        interactionUI.SetActive(false);
                        yield break;
                    }
                }
                else if (hit.collider.CompareTag("NPC"))
                {
                    interactionUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<NPC>().StartInteraction();
                        interactionUI.SetActive(false);
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
                GetOutTheCar();
                yield break;
            }
            yield return null;
        }
    }

    private void GetInTheCar()
    {
        if (car)
        {
            cursor.enabled = false;
            
            rccCarControllerV3.enabled = true;
            rccCameraObject.SetActive(true);
            rccCamera.cameraTarget.playerVehicle = rccCarControllerV3;
            
            movement.SetActive(false);
            StartCoroutine(Car());
            Debug.Log("Get in the car");
        }
    }
    private void GetOutTheCar()
    {
        if (car)
        {
            cursor.enabled = true;
            
            rccCameraObject.SetActive(false);
            rccCarControllerV3.enabled = false;
            
            movement.SetActive(true);
            movement.transform.position = car.transform.position + new Vector3(2, 0, 2);
            StartCoroutine(Player());
            Debug.Log("Get out the car");
        }
    }
    public void DisablePlayer(bool trade, CarAttributes carAttributes = null, GameObject NPC = null)
    {
        firstPersonMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (trade)
        {
            carTradeUI.SetActive(true);
            currentTradeCarAttributes = carAttributes;
            currentNPC = NPC;
            ShowCarAttributesUI();
        }
        Debug.Log("Player disabled");
    }
    public void EnablePlayer(bool trade)
    {
        firstPersonMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Player());
        if (trade) {carTradeUI.SetActive(false);}
        Debug.Log("Player enabled");
    }

    private void ShowCarAttributesUI()
    {
        carNameText.text = currentTradeCarAttributes.carModelName;
        carModelYearText.text = currentTradeCarAttributes.carModelYear.ToString();
        carKilometerText.text = currentTradeCarAttributes.carKilometer.ToString();
        carFuelText.text = currentTradeCarAttributes.carFuelType == 0 ? "Gasoline" : "Diesel";
        carGearText.text = currentTradeCarAttributes.carGearType == 0 ? "Manual" : "Automatic";
        carPriceText.text = currentTradeCarAttributes.salePrice.ToString();
        carColorImage.color = currentTradeCarAttributes.carColor;
        
        carBodyDamageImage.color = currentTradeCarAttributes.bodyDamagePercentage > 0 ? damagedCarColorImage.color : currentTradeCarAttributes.isBodyPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carFrontDamageImage.color = currentTradeCarAttributes.frontDamagePercentage > 0 ? damagedCarColorImage.color : currentTradeCarAttributes.isFrontPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carRearDamageImage.color = currentTradeCarAttributes.rearDamagePercentage > 0 ? damagedCarColorImage.color : currentTradeCarAttributes.isRearPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carLeftDamageImage.color = currentTradeCarAttributes.leftDamagePercentage > 0 ? damagedCarColorImage.color : currentTradeCarAttributes.isLeftPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        carRightDamageImage.color = currentTradeCarAttributes.rightDamagePercentage > 0 ? damagedCarColorImage.color : currentTradeCarAttributes.isRightPaintedBefore ? paintedCarColorImage.color : originalCarColorImage.color;
        
        carBodyDamageText.text = currentTradeCarAttributes.bodyDamagePercentage == 0 ? "Body" : "Body " + currentTradeCarAttributes.bodyDamagePercentage + "%";
        carFrontDamageText.text = currentTradeCarAttributes.frontDamagePercentage == 0 ? "Front" : "Front " + currentTradeCarAttributes.frontDamagePercentage + "%";
        carRearDamageText.text = currentTradeCarAttributes.rearDamagePercentage == 0 ? "Rear" : "Rear " + currentTradeCarAttributes.rearDamagePercentage + "%";
        carLeftDamageText.text = currentTradeCarAttributes.leftDamagePercentage == 0 ? "Left" : "Left " + currentTradeCarAttributes.leftDamagePercentage + "%";
        carRightDamageText.text = currentTradeCarAttributes.rightDamagePercentage == 0 ? "Right" : "Right " + currentTradeCarAttributes.rightDamagePercentage + "%";
    }
    
    public void TradeCar()
    {
        if (currentTradeCarAttributes)
        {
            if (currentTradeCarAttributes.salePrice <= PlayerPrefs.GetInt("Money"))
            {
                playerInventory.SubtractMoney(currentTradeCarAttributes.salePrice);
                PlayerPrefs.SetString("Car", currentTradeCarAttributes.name);
                currentTradeCarAttributes.isPlayerBought = true;
                EnablePlayer(true);
                Destroy(currentNPC);
                Debug.Log("Car bought");
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}
