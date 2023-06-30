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
    
    public void SetInfo(CarAttributes carAttributes)
    {
        carModelName.text = "Model: " + carAttributes.carModelName;
        carUniqueKey.text = "Unique Car Key: " + carAttributes.carKey.ToString();
        priceInputField.placeholder.GetComponent<TMP_Text>().text = "Example: " + carAttributes.salePrice.ToString() + "..";
        isCarOnSaleToggle.isOn = false;
        saleCarAttributes = carAttributes;
    }
}
