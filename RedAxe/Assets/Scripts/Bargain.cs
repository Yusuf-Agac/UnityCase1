using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bargain : MonoBehaviour
{
    public ChatBox chatBox;
    private PlayerActions _playerActions;

    private void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
    }

    public void StartBargain (bool isPlayerBargain, int sellerPrice)
    {
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Initial), isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Initial), !isPlayerBargain);
        chatBox.AddMessage(AddPriceToMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.SellerPrice), sellerPrice), !isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Thinking), isPlayerBargain);
    }
    
    public void AcceptBargain (bool isPlayerBargain)
    {
        if (isPlayerBargain)
        {
            _playerActions.BuyCar();
        }
        else
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Accept), false);
        }
    }
    
    public void BargainRequest (bool isPlayerBargain, int requestedPrice)
    {
        var message = AddPriceToMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Bargain), requestedPrice);
        chatBox.AddMessage(message, isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Thinking), !isPlayerBargain);
    }

    private static string AddPriceToMessage (string message, int price)
    {
        return message.Replace("$", price.ToString()+"$");
    }
}
