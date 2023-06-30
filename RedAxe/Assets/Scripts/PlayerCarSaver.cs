using UnityEngine;

public static class PlayerCarSaver
{
    public static void SaveCarAttributes(CarAttributes carAttributes)
    {
        int carCount = PlayerPrefs.GetInt("CarCount", -1);
        carCount++;
        PlayerPrefs.SetInt("CarCount", carCount);
        
        string carKey = "Car" + carCount.ToString();
        string carData = JsonUtility.ToJson(carAttributes.ToData());
        PlayerPrefs.SetString(carKey, carData);
    }
}