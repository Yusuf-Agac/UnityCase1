using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarGenerator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<Transform> spawnPoints;
    private float frontDamagePercentage;
    private float rearDamagePercentage;
    private float leftDamagePercentage;
    private float rightDamagePercentage;
    
    private GameObject car;
    void Start()
    {
        foreach (var pos in spawnPoints)
        {
            car = Instantiate(prefabs[0], pos.position, pos.rotation);
            var carAttributes = car.GetComponent<CarAttributes>();
            CreateRandomDamage(carAttributes);
            carAttributes.StartModification();
        }
    }

    private void CreateRandomDamage(CarAttributes carAttributes)
    {
        const float possibility = 0.5f;

        CreateRandomPartDamage(carAttributes, "front", possibility);
        CreateRandomPartDamage(carAttributes, "rear", possibility);
        CreateRandomPartDamage(carAttributes, "left", possibility);
        CreateRandomPartDamage(carAttributes, "right", possibility);
    }

    private void CreateRandomPartDamage(CarAttributes carAttributes, string partName, float possibility)
    {
        if (Random.Range(0, 1 / possibility) == 0)
        {
            SetPartDamage(carAttributes, partName);
            carAttributes.SetPartPaintedBefore(partName, true);
        }
        else if (Random.Range(0, 1 / possibility) == 0)
        {
            carAttributes.SetPartPaintedBefore(partName, true);
        }
        else
        {
            carAttributes.SetPartPaintedBefore(partName, false);
        }
    }

    private void SetPartDamage(CarAttributes carAttributes, string partName)
    {
        switch (partName)
        {
            case "front":
                carAttributes.frontDamagePercentage = Random.Range(0, 100);
                break;
            case "rear":
                carAttributes.rearDamagePercentage = Random.Range(0, 100);
                break;
            case "left":
                carAttributes.leftDamagePercentage = Random.Range(0, 100);
                break;
            case "right":
                carAttributes.rightDamagePercentage = Random.Range(0, 100);
                break;
        }
    }
}
