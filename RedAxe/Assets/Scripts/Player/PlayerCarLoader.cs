using Car;
using UnityEngine;

namespace Player
{
    public static class PlayerCarLoader
    {
        public static CarAttributesData LoadCarAttributes(int carIndex)
        {
            if (HasCar(carIndex))
            {
                string carData = PlayerPrefs.GetString("Car" + carIndex.ToString());
                return JsonUtility.FromJson<CarAttributesData>(carData);
            }

            return null;
        }
    
        public static bool HasCar(int carIndex)
        {
            string carKey = "Car" + carIndex.ToString();
            return PlayerPrefs.HasKey(carKey);
        }
    }
}