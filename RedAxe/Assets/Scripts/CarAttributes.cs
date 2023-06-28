using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CarAttributes : MonoBehaviour
{
    [Space(20)]
    public bool isPlayerBought = false;
    [Space(20)]
    public int basePrice;
    [ReadOnly] public int salePrice;
    [Space(20)]
    [ReadOnly] public float bodyDamagePercentage;
    [ReadOnly] public float frontDamagePercentage;
    [ReadOnly] public float rearDamagePercentage;
    [ReadOnly] public float leftDamagePercentage;
    [ReadOnly] public float rightDamagePercentage;
    [Space(20)]
    [ReadOnly] public bool isBodyPaintedBefore;
    [ReadOnly] public bool isFrontPartPaintedBefore;
    [ReadOnly] public bool isRearPartPaintedBefore;
    [ReadOnly] public bool isLeftPartPaintedBefore;
    [ReadOnly] public bool isRightPartPaintedBefore;
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
        if(isFrontPartPaintedBefore){MaterialModifier.ChangeMaterialColor(frontPartRenderer, frontPartMaterialIndex);}
        if(isRearPartPaintedBefore){MaterialModifier.ChangeMaterialColor(rearPartRenderer, rearPartMaterialIndex);}
        if(isLeftPartPaintedBefore){MaterialModifier.ChangeMaterialColor(leftPartRenderer, leftPartMaterialIndex);}
        if(isRightPartPaintedBefore){MaterialModifier.ChangeMaterialColor(rightPartRenderer, rightPartMaterialIndex);}
        
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
                isFrontPartPaintedBefore = isPaintedBefore;
                break;
            case "rear":
                isRearPartPaintedBefore = isPaintedBefore;
                break;
            case "left":
                isLeftPartPaintedBefore = isPaintedBefore;
                break;
            case "right":
                isRightPartPaintedBefore = isPaintedBefore;
                break;
        }
    }
}
