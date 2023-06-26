using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int money = 10000;
    public TMP_Text moneyText;

    private void Start()
    {
        PrintMoney();
    }

    private void PrintMoney()
    {
        moneyText.text = money.ToString() + " $";
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
        PrintMoney();
    }
    
    public void SubtractMoney(int amount)
    {
        money -= amount;
        PrintMoney();
    }
}
