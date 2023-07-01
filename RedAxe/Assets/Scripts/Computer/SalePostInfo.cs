using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalePostInfo : MonoBehaviour
{
    public TMP_Text carModelName;
    public TMP_Text carUniqueKey;
    public TMP_InputField priceInputField;
    public Toggle isCarOnSaleToggle;
    public CarAttributes saleCarAttributes;
    private int tempPrice;
    
    public void SetInfo(CarAttributes carAttributes)
    {
        carModelName.text = "Model: " + carAttributes.carModelName;
        carUniqueKey.text = "Unique Car Key: " + carAttributes.carKey.ToString();
        priceInputField.placeholder.GetComponent<TMP_Text>().text = "Change: " + carAttributes.salePrice.ToString() + "..";
        isCarOnSaleToggle.isOn = false;
        saleCarAttributes = carAttributes;
        tempPrice = carAttributes.salePrice;
    }
    
    public void ToggleCarOnSale()
    {
        if (saleCarAttributes == null) { return; }
        if (isCarOnSaleToggle.isOn)
        {
            bool isParsed = int.TryParse(priceInputField.text, out var parsedPrice);

            saleCarAttributes.salePrice = isParsed ? parsedPrice : tempPrice;
            priceInputField.interactable = false;
            saleCarAttributes.GetComponent<BuyerNpcGenerator>().StartGenerating();
        }
        else
        {
            priceInputField.text = "";
            saleCarAttributes.salePrice = tempPrice;
            priceInputField.interactable = true;
            saleCarAttributes.GetComponent<BuyerNpcGenerator>().isGenerating = false;
        }
    }

}
