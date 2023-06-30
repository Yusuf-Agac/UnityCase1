using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarGenerator : MonoBehaviour
{
    public List<GameObject> carPrefabs;
    public List<string> carModelNames;
    void Start()
    {
        int carCount = PlayerPrefs.GetInt("CarCount", 0);
        for (int i = 0; i <= carCount; i++)
        {
            if (!PlayerCarLoader.HasCar(i))
            {
                Debug.Log("Car " + i + " not found");
                continue;
            }
            CarAttributesData carAttributes = PlayerCarLoader.LoadCarAttributes(i);
            Debug.Log("Car detected: " + carAttributes.carModelName);
            int carIndex = carModelNames.IndexOf(carAttributes.carModelName);
            if (carIndex == -1)
            {
                Debug.Log("Car model not found");
                continue;
            }
            var car = Instantiate(carPrefabs[carIndex], transform.position, transform.rotation);
            car.transform.localPosition += Vector3.right * i * 3f;
            var carAttributesComponent = car.GetComponent<CarAttributes>();
            carAttributesComponent.FromData(carAttributes);
            carAttributesComponent.StartModification();
        }
    }
}
