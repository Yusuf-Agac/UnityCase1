using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerActions : MonoBehaviour
{
    public Transform contentTransform;
    public GameObject carSellingBoxPrefab;
    
    public void AddCarToSellingBox(CarAttributes carAttributes)
    {
        var carSellingBox = Instantiate(carSellingBoxPrefab, contentTransform);
        carSellingBox.GetComponent<SalePostInfo>().SetInfo(carAttributes);
        carSellingBox.name = carAttributes.carKey.ToString();
    }
    
    public void RemoveCarFromSellingBox(int carKey)
    {
        var carSellingBox = contentTransform.Find(carKey.ToString()).gameObject;
        Destroy(carSellingBox);
    }
}
