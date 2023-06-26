using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    public CarAttributes carAttributes;
    private GameObject carTradeUI;

    private void Awake()
    {
        carTradeUI = GameObject.FindGameObjectWithTag("TradeUI");
    }

    public void StartInteraction()
    {
        carTradeUI.SetActive(true);
    }
}
