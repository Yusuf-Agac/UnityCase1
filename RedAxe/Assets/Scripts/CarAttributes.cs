using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CarAttributes : MonoBehaviour
{
    public int basePrice;
    [ReadOnly] public float frontDamagePercentage;
    [ReadOnly] public float rearDamagePercentage;
    [ReadOnly] public float leftDamagePercentage;
    [ReadOnly] public float rightDamagePercentage;
    [ReadOnly] public bool isFrontPartDamagedBefore;
    [ReadOnly] public bool isRearPartDamagedBefore;
    [ReadOnly] public bool isLeftPartDamagedBefore;
    [ReadOnly] public bool isRightPartDamagedBefore;
}
