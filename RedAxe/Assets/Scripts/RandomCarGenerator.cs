using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarGenerator : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<Transform> spawnPoints;

    public GameObject customerPrefab;

    void Start()
    {
        foreach (var pos in spawnPoints)
        {
            var car = Instantiate(prefabs[Random.Range(0, prefabs.Count)], pos.position, pos.rotation);
            var npc = pos.GetChild(0);
            npc.transform.localPosition += Vector3.left * 1.5f;
            npc.GetComponent<Npc>().carAttributes = car.GetComponent<CarAttributes>();
            var carAttributes = car.GetComponent<CarAttributes>();
            CreateRandomDamage(carAttributes);
            carAttributes.StartModification();
        }
    }

    private void CreateRandomDamage(CarAttributes carAttributes)
    {
        float possibility = Random.Range(0.05f, 0.3f);
        
        CreateRandomPartDamage(carAttributes, "body", possibility);
        CreateRandomPartDamage(carAttributes, "front", possibility);
        CreateRandomPartDamage(carAttributes, "rear", possibility);
        CreateRandomPartDamage(carAttributes, "left", possibility);
        CreateRandomPartDamage(carAttributes, "right", possibility);
    }

    private void CreateRandomPartDamage(CarAttributes carAttributes, string partName, float possibility)
    {
        if ((int)Random.Range(0, 1 - (1 / possibility)) == 0)
        {
            SetPartDamage(carAttributes, partName);
            carAttributes.SetPartPaintedBefore(partName, true);
        }
        else if ((int)Random.Range(0, 1 - (1 / (possibility * 0.1f))) == 0)
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
            case "body":
                carAttributes.bodyDamagePercentage = Random.Range(0, 100);
                break;
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
