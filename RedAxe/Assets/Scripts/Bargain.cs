using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bargain : MonoBehaviour
{
    public ChatBox chatBox;
    private PlayerActions _playerActions;
    private NPC _npc;

    private void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
    }

    public void StartBargain (bool isPlayerBargain, int sellerPrice, NPC npc)
    {
        _npc = npc;
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Initial), isPlayerBargain);
        if (isPlayerBargain && _npc.angryNotGonnaSell)
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry), false);
            return;
        }
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
    
    public void BargainRequest (bool isPlayerBargain, int requestedPrice)
    {
        if (isPlayerBargain && _npc.angryNotGonnaSell)
        {
            chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry), false);
            return;
        }
        chatBox.AddMessage(AddPriceToMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Bargain), requestedPrice), isPlayerBargain);
        chatBox.AddMessage(BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Thinking), !isPlayerBargain);
        if (isPlayerBargain)
        {
            chatBox.AddMessage(_npc.BargainAnswer(requestedPrice) == 0 ? BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Accept) : _npc.BargainAnswer(requestedPrice) == 1 ? BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Reject) : BargainCommunication.GetRandomMessage(BargainCommunication.BargainState.Angry), false);
        }
        else
        {
            // TODO: Add player answer
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
