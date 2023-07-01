using Car;
using Player;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public enum NpcType
    {
        seller,
        buyer
    }
    
    public NpcType npcType = NpcType.seller;
    private RectTransform _typeTextRectTransform;
    
    public CarAttributes carAttributes;
    
    [ReadOnly] public bool angryNotGonnaSell = false;
    private readonly Vector2 _randomToleranceRange = new Vector2(0.05f, 0.3f);
    
    private FirstPersonMovement _playerMovement;
    private PlayerActions _playerActions;

    private void Awake()
    {
        _playerMovement = GameObject.FindWithTag("Movement").GetComponent<FirstPersonMovement>();
        _playerActions = _playerMovement.transform.parent.GetComponent<PlayerActions>();
        _typeTextRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        CanvasTagLookPlayer();
    }
    
    public void StartInteraction()
    {
        _playerActions.DisablePlayer(true, carAttributes, gameObject, npcType);
    }

    private void CanvasTagLookPlayer()
    {
        _typeTextRectTransform.LookAt(_playerMovement.transform);
        _typeTextRectTransform.Rotate(0, 180, 0);
    }
    
    public void ResetNpcTypeCanvas()
    {
        switch (npcType)
        {
            case NpcType.buyer:
                _typeTextRectTransform.transform.GetChild(0).GetComponent<TMP_Text>().text = "Buyer";
                break;
            case NpcType.seller:
                _typeTextRectTransform.transform.GetChild(0).GetComponent<TMP_Text>().text = "Seller";
                break;
            default:
                _typeTextRectTransform.transform.GetChild(0).GetComponent<TMP_Text>().text = "Seller";
                break;
        }
    }
    
    public int BargainOffer(int price)
    {
        float discount = Random.Range(0f, 0.5f);
        return price - (int)(price * discount);
    }
    
    public int BargainAnswer(int bargain)
    {
        float tolerance = Random.Range(_randomToleranceRange.x, _randomToleranceRange.y);
        if (angryNotGonnaSell || Random.Range(0, 5) == 0 || carAttributes.salePrice / (float)bargain - 1 > 0.65f)
        {
            angryNotGonnaSell = true;
            return 2;
        }
        
        if ((carAttributes.salePrice / (float)bargain) - 1 <= tolerance)
        {
            carAttributes.bargainPrice = bargain;
            return 0;
        }
        else { return 1; }
    }
}
