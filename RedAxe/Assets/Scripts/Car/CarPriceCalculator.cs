namespace Car
{
    public static class CarPriceCalculator
    {
        private const float BodyPriceMultiplier = 0.25f;
        private const float FrontPriceMultiplier = 0.125f;
        private const float RearPriceMultiplier = 0.125f;
        private const float LeftPriceMultiplier = 0.06f;
        private const float RightPriceMultiplier = 0.06f;
    
        private const float PaintedPriceMultiplier = 0.15f;
    
        private const float SpeedPriceMultiplier = 0.00005f;
        private const float TorquePriceMultiplier = 0.000025f;
    
        public static int CalculatePrice(CarAttributes carAttributes, RCC_CarControllerV3 carController)
        {
            float result = carAttributes.basePrice;
            result -= (carAttributes.bodyDamagePercentage/100) * carAttributes.basePrice * BodyPriceMultiplier;
            result -= (carAttributes.frontDamagePercentage/100) * carAttributes.basePrice * FrontPriceMultiplier;
            result -= (carAttributes.rearDamagePercentage/100) * carAttributes.basePrice * RearPriceMultiplier;
            result -= (carAttributes.leftDamagePercentage/100) * carAttributes.basePrice * LeftPriceMultiplier;
            result -= (carAttributes.rightDamagePercentage/100) * carAttributes.basePrice * RightPriceMultiplier;
        
            if (carAttributes.isBodyPaintedBefore){result -= carAttributes.basePrice * BodyPriceMultiplier * PaintedPriceMultiplier;}
            if (carAttributes.isFrontPaintedBefore){result -= carAttributes.basePrice * FrontPriceMultiplier * PaintedPriceMultiplier;}
            if (carAttributes.isRearPaintedBefore){result -= carAttributes.basePrice * RearPriceMultiplier * PaintedPriceMultiplier;}
            if (carAttributes.isLeftPaintedBefore){result -= carAttributes.basePrice * LeftPriceMultiplier * PaintedPriceMultiplier;}
            if (carAttributes.isRightPaintedBefore){result -= carAttributes.basePrice * RightPriceMultiplier * PaintedPriceMultiplier;}
        
            result += (carController.maxspeed * carAttributes.basePrice) * SpeedPriceMultiplier;
            result += (carController.maxEngineTorque * carAttributes.basePrice) * TorquePriceMultiplier;
        
            return (int)result;
        }
    }
}
