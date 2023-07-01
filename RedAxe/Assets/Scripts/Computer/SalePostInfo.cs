using Car;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Computer
{
    public class SalePostInfo : MonoBehaviour
    {
        public TMP_Text carModelName;
        public TMP_Text carUniqueKey;
        public TMP_InputField priceInputField;
        public Toggle isCarOnSaleToggle;
        public CarAttributes carAttributes;
    
        private int _tempPrice;
    
        public void SetInfo(CarAttributes parameterCarAttributes)
        {
            carModelName.text = "Model: " + parameterCarAttributes.carModelName;
            carUniqueKey.text = "Unique Car Key: " + parameterCarAttributes.carKey.ToString();
            priceInputField.placeholder.GetComponent<TMP_Text>().text = "Change: " + parameterCarAttributes.salePrice.ToString() + "..";
            isCarOnSaleToggle.isOn = false;
            carAttributes = parameterCarAttributes;
            _tempPrice = parameterCarAttributes.salePrice;
        }
    
        public void ToggleCarOnSale()
        {
            if (carAttributes == null) { return; }
            if (isCarOnSaleToggle.isOn)
            {
                bool isParsed = int.TryParse(priceInputField.text, out var parsedPrice);

                carAttributes.salePrice = isParsed ? parsedPrice : _tempPrice;
                priceInputField.interactable = false;
                carAttributes.GetComponent<BuyerNpcGenerator>().StartGenerating();
            }
        
            else
            {
                priceInputField.text = "";
                carAttributes.salePrice = _tempPrice;
                priceInputField.interactable = true;
                carAttributes.GetComponent<BuyerNpcGenerator>().isGenerating = false;
            }
        }
    }
}
