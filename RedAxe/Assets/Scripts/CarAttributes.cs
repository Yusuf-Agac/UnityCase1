using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CarAttributes : MonoBehaviour
{
    public enum CarGearType {Automatic, Manual}
    public enum CarFuelType {Gasoline, Diesel}

    [Space(20)]
    public bool isPlayerBought = false;
    [Space(20)]
    public int basePrice;
    [ReadOnly] public int salePrice;
    [Space(40)]
    public string carModelName;
    public int carModelYear;
    public int carKilometer;
    public CarFuelType carFuelType;
    public CarGearType carGearType;
    public Color carColor;
    [Space(40)]
    [ReadOnly] public float bodyDamagePercentage;
    [ReadOnly] public float frontDamagePercentage;
    [ReadOnly] public float rearDamagePercentage;
    [ReadOnly] public float leftDamagePercentage;
    [ReadOnly] public float rightDamagePercentage;
    [Space(20)]
    [ReadOnly] public bool isBodyPaintedBefore;
    [ReadOnly] public bool isFrontPaintedBefore;
    [ReadOnly] public bool isRearPaintedBefore;
    [ReadOnly] public bool isLeftPaintedBefore;
    [ReadOnly] public bool isRightPaintedBefore;
    [Space(20)]
    public List<Renderer> bodyPartRenderer;
    public List<int> bodyPartMaterialIndex;
    [Space(40)]
    public List<Renderer> frontPartRenderer;
    public List<int> frontPartMaterialIndex;
    [Space(40)]
    public List<Renderer> rearPartRenderer;
    public List<int> rearPartMaterialIndex;
    [Space(40)]
    public List<Renderer> leftPartRenderer;
    public List<int> leftPartMaterialIndex;
    [Space(40)]
    public List<Renderer> rightPartRenderer;
    public List<int> rightPartMaterialIndex;

    public void StartModification()
    {
        if(bodyDamagePercentage != 0){MaterialModifier.ChangeNormalMap(bodyDamagePercentage/100, bodyPartRenderer, bodyPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(frontDamagePercentage/100, frontPartRenderer, frontPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(rearDamagePercentage/100, rearPartRenderer, rearPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(leftDamagePercentage/100, leftPartRenderer, leftPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(rightDamagePercentage/100, rightPartRenderer, rightPartMaterialIndex);}
        
        if(isBodyPaintedBefore){MaterialModifier.ChangeMaterialColor(bodyPartRenderer, bodyPartMaterialIndex);}
        if(isFrontPaintedBefore){MaterialModifier.ChangeMaterialColor(frontPartRenderer, frontPartMaterialIndex);}
        if(isRearPaintedBefore){MaterialModifier.ChangeMaterialColor(rearPartRenderer, rearPartMaterialIndex);}
        if(isLeftPaintedBefore){MaterialModifier.ChangeMaterialColor(leftPartRenderer, leftPartMaterialIndex);}
        if(isRightPaintedBefore){MaterialModifier.ChangeMaterialColor(rightPartRenderer, rightPartMaterialIndex);}
        
        salePrice = CarPriceCalculator.CalculatePrice(this, GetComponent<RCC_CarControllerV3>());
    }
    
public void SetPartPaintedBefore(string partName, bool isPaintedBefore)
    {
        switch (partName)
        {
            case "body":
                isBodyPaintedBefore = isPaintedBefore;
                break;
            case "front":
                isFrontPaintedBefore = isPaintedBefore;
                break;
            case "rear":
                isRearPaintedBefore = isPaintedBefore;
                break;
            case "left":
                isLeftPaintedBefore = isPaintedBefore;
                break;
            case "right":
                isRightPaintedBefore = isPaintedBefore;
                break;
        }
    }
}
