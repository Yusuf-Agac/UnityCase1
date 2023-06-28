using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [ReadOnly] public int money = 10000;
    public TMP_Text moneyText;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("Money", 10000);
        PrintMoney();
        PlayerPrefs.SetInt("Money", money);
    }

    private void PrintMoney()
    {
        moneyText.text = money.ToString() + " $";
    }
    
    public void AddMoney(int amount)
    {
        money += amount;
        PlayerPrefs.SetInt("Money", money);
        PrintMoney();
    }
    
    public void SubtractMoney(int amount)
    {
        money -= amount;
        PlayerPrefs.SetInt("Money", money);
        PrintMoney();
    }
}
