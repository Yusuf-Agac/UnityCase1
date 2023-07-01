using UnityEngine;

namespace Car
{
    [System.Serializable]
    public class CarAttributesData
    {
        public bool isPlayerBought = false;
        public int carKey;
    
        public int basePrice;
        public int salePrice;
        public int bargainPrice;
    
        public string carModelName;
        public int carModelYear;
        public int carKilometer;
        public int carFuelType;
        public int carGearType;
        public Color carColor;
    
        public float bodyDamagePercentage;
        public float frontDamagePercentage;
        public float rearDamagePercentage;
        public float leftDamagePercentage;
        public float rightDamagePercentage;
    
        public bool isBodyPaintedBefore;
        public bool isFrontPaintedBefore;
        public bool isRearPaintedBefore;
        public bool isLeftPaintedBefore;
        public bool isRightPaintedBefore;
    }
}
