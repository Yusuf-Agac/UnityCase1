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
            car = Instantiate(prefabs[Random.Range(0, prefabs.Count)], pos.position, pos.rotation);
            var carAttributes = car.GetComponent<CarAttributes>();
            
        }
    }

    private void CreateRandomDamage(CarAttributes carAttributes)
    {
        if (Random.Range(0, 2) == 0)
        {
            carAttributes.frontDamagePercentage = Random.Range(0, 100);
            carAttributes.isFrontPartDamagedBefore = true;
        }
        else if(Random.Range(0, 2) == 0)
        {
            carAttributes.isFrontPartDamagedBefore = true;
        }
        else
        {
            carAttributes.isFrontPartDamagedBefore = false;
        }
        if (Random.Range(0, 2) == 0)
        {
            carAttributes.rearDamagePercentage = Random.Range(0, 100);
            carAttributes.isRearPartDamagedBefore = true;
        }
        else if(Random.Range(0, 2) == 0)
        {
            carAttributes.isRearPartDamagedBefore = true;
        }
        else
        {
            carAttributes.isRearPartDamagedBefore = false;
        }
        if (Random.Range(0, 2) == 0)
        {
            carAttributes.leftDamagePercentage = Random.Range(0, 100);
            carAttributes.isLeftPartDamagedBefore = true;
        }
        else if(Random.Range(0, 2) == 0)
        {
            carAttributes.isLeftPartDamagedBefore = true;
        }
        else
        {
            carAttributes.isLeftPartDamagedBefore = false;
        }
        if (Random.Range(0, 2) == 0)
        {
            carAttributes.rightDamagePercentage = Random.Range(0, 100);
            carAttributes.isRightPartDamagedBefore = true;
        }
        else if(Random.Range(0, 2) == 0)
        {
            carAttributes.isRightPartDamagedBefore = true;
        }
        else
        {
            carAttributes.isRightPartDamagedBefore = false;
        }
    }
}
