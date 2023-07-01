using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bargain : MonoBehaviour
{
    public ChatBox chatBox;
    
    private PlayerActions _playerActions;
    private Npc _npc;

    private void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
    }

    public void StartBargain (bool isPlayerBargain, int price, Npc npc)
    {
        _npc = npc;
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Initial), isPlayerBargain);
        if (isPlayerBargain && _npc.angryNotGonnaSell)
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry), false);
            return;
        }
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Initial), !isPlayerBargain);
        chatBox.AddMessage(AddPriceToMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.SellerPrice), price), !isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Thinking), isPlayerBargain);
        if (!isPlayerBargain) { OfferBargain(false, npc.BargainOffer(price)); }
    }
    
    public void AcceptBargain (bool isPlayerBargain)
    {
        if (isPlayerBargain)
        {
            _playerActions.BuyCar();
        }
        else
        {
            _playerActions.SellCar();
        }
    }
    
    public void RejectBargain (bool isPlayerBargain)
    {
        if (isPlayerBargain)
        {
            _playerActions.DisablePlayer(false);
        }
        else
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Reject), false);
        }
    }
    
    public void OfferBargain (bool isPlayerBargain, int requestedPrice)
    {
        if(!isPlayerBargain) { _npc.carAttributes.bargainPrice = requestedPrice; }
        if (isPlayerBargain && _npc.angryNotGonnaSell)
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry), false);
            return;
        }
        chatBox.AddMessage(AddPriceToMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Bargain), requestedPrice), isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Thinking), !isPlayerBargain);
        if (isPlayerBargain)
        {
            int result = _npc.BargainAnswer(requestedPrice);
            chatBox.AddMessage(result switch
            {
                0 => BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Accept),
                1 => BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Reject),
                _ => BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry)
            }, false);
        }
    }
    
    public void CloseBargain ()
    {
        _npc.carAttributes.ResetBargainPrice();
    }

    private static string AddPriceToMessage (string message, int price)
    {
        return message.Replace("$", price.ToString()+"$");
    }
}
