using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NpcBuyerGenerator : MonoBehaviour
{
    public GameObject npcPrefab;
    [ReadOnly] public bool isGenerating = false;
    private CarAttributes carAttributes;

    private void Start()
    {
        carAttributes = GetComponent<CarAttributes>();
    }

    public void StartGenerating()
    {
        if (isGenerating) { return; }
        StartCoroutine(GenerateNpc());
    }
    
    IEnumerator GenerateNpc()
    {
        isGenerating = true;
        while (isGenerating)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));
            if(Random.Range(0, 3) == 0) 
            { 
                var npc = Instantiate(npcPrefab, transform.position, transform.rotation);
                var npcComponent = npc.GetComponent<Npc>();
                npc.transform.localPosition += Vector3.forward * 1.5f + Vector3.left * 1.5f;
                npcComponent.npcType = Npc.NpcType.buyer;
                npcComponent.carAttributes = carAttributes;
                npcComponent.ResetNpcTypeCanvas();
                npc.transform.parent = null;
                npc.transform.position = new Vector3(npc.transform.position.x, 0.65f, npc.transform.position.z);
                isGenerating = false;
                yield break;
            }
        }
    }
}