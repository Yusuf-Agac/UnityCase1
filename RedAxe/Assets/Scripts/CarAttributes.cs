using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CarAttributes : MonoBehaviour
{
    public int basePrice;
    [ReadOnly] public float frontDamagePercentage;
    [ReadOnly] public float rearDamagePercentage;
    [ReadOnly] public float leftDamagePercentage;
    [ReadOnly] public float rightDamagePercentage;
    [ReadOnly] public bool isFrontPartPaintedBefore;
    [ReadOnly] public bool isRearPartPaintedBefore;
    [ReadOnly] public bool isLeftPartPaintedBefore;
    [ReadOnly] public bool isRightPartPaintedBefore;
    
    public List<Renderer> frontPartRenderer;
    public Material frontPartMaterial;
    public List<int> frontPartMaterialIndex;
    
    public List<Renderer> rearPartRenderer;
    public Material rearPartMaterial;
    public List<int> rearPartMaterialIndex;
    
    public List<Renderer> leftPartRenderer;
    public Material leftPartMaterial;
    public List<int> leftPartMaterialIndex;
    
    public List<Renderer> rightPartRenderer;
    public Material rightPartMaterial;
    public List<int> rightPartMaterialIndex;

    public void StartModification()
    {
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(frontDamagePercentage/100, frontPartRenderer, frontPartMaterial, frontPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(rearDamagePercentage/100, rearPartRenderer, rearPartMaterial, rearPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(leftDamagePercentage/100, leftPartRenderer, leftPartMaterial, leftPartMaterialIndex);}
        if(frontDamagePercentage != 0){MaterialModifier.ChangeNormalMap(rightDamagePercentage/100, rightPartRenderer, rightPartMaterial, rightPartMaterialIndex);}
        
        if(isFrontPartPaintedBefore){MaterialModifier.ChangeMaterialColor(frontPartRenderer, frontPartMaterial, frontPartMaterialIndex);}
        if(isRearPartPaintedBefore){MaterialModifier.ChangeMaterialColor(rearPartRenderer, rearPartMaterial, rearPartMaterialIndex);}
        if(isLeftPartPaintedBefore){MaterialModifier.ChangeMaterialColor(leftPartRenderer, leftPartMaterial, leftPartMaterialIndex);}
        if(isRightPartPaintedBefore){MaterialModifier.ChangeMaterialColor(rightPartRenderer, rightPartMaterial, rightPartMaterialIndex);}
    }
    
public void SetPartPaintedBefore(string partName, bool isPaintedBefore)
    {
        switch (partName)
        {
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
