using UnityEngine;

public class NPC : MonoBehaviour
{
    public CarAttributes carAttributes;
    [ReadOnly] public bool angryNotGonnaSell = false;
    
    private readonly Vector2 _randomToleranceRange = new Vector2(0.05f, 0.3f);
    private FirstPersonMovement _playerMovement;
    private PlayerActions _playerActions;
    private RectTransform _canvasRectTransform;

    private void Awake()
    {
        _playerMovement = GameObject.FindWithTag("Movement").GetComponent<FirstPersonMovement>();
        _playerActions = _playerMovement.transform.parent.GetComponent<PlayerActions>();
        _canvasRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }
    
    private void Update()
    {
        CanvasTagLookPlayer();
    }
    
    public void StartInteraction()
    {
        _playerActions.DisablePlayer(true, carAttributes, gameObject);
    }

    private void CanvasTagLookPlayer()
    {
        _canvasRectTransform.LookAt(_playerMovement.transform);
        _canvasRectTransform.Rotate(0, 180, 0);
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
