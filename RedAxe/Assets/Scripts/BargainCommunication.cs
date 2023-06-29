using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BargainCommunication
{
    public enum BargainState
    {
        Initial,
        Thinking,
        SellerPrice,
        Bargain,
        Accept,
        Reject
    }
    
    private static readonly string[] InitialMessages = new string[]
    {
        "Hello",
        "Hi",
        "What's up?",
        "How are you?",
        "How's it going?"
    };

    private static readonly string[] ThinkingMessages = new string[]
    {
        "Let me think...",
        "Hmm...",
        "Let me see...",
        "Let me check...",
        "Let me calculate...",
        "Let me do some math...",
        "Let me do some calculations..."
    };

    private static readonly string[] SellerPriceMessages = new string[]
    {
        "My friend you are gonna be happy with this price, it's only $.",
        "It's only $, you can't find a better deal.",
        "Trust me, its best price you can find, it's only $."
    };
    
    private static readonly string[] BargainMessages = new string[]
    {
        "I can give you $ for this car.",
        "I can give you $ for this car. What do you say?",
        "This car is worth $.",
        "Okay, I can give you $.",
        "But this is too much, it's worth $.",
        "Can you give me a better price? I can give you $."
    };
    
    private static readonly string[] AcceptMessages = new string[]
    {
        "Okay, deal.",
        "Okay, it's a deal.",
        "Okay, it's a deal. Pleasure doing business with you.",
        "Pleasure doing business with you. Have a nice day.",
        "Have a nice day. See you later.",
        "Okay, it's a deal. Pleasure doing business with you. Have a nice day. See you later. Bye.",
        "See you later. Bye. See you next time.",
        "Bye. See you next time. See you soon.",
        "See you next time. See you soon. See you tomorrow.",
        "See you soon. See you tomorrow. See you next week."
    };
    
    private static readonly string[] RejectMessages = new string[]
    {
        "No, it's too low.",
        "I can't sell it for this price.",
        "Can you give me a better price?",
        "I can't go any lower.",
        "What are you talking about?",
        "OMG are you kidding me?"
    };
    
    public static string GetRandomMessage(BargainState state)
    {
        switch (state)
        {
            case BargainState.Initial:
                return InitialMessages[Random.Range(0, InitialMessages.Length)];
            case BargainState.Thinking:
                return ThinkingMessages[Random.Range(0, ThinkingMessages.Length)];
            case BargainState.SellerPrice:
                return SellerPriceMessages[Random.Range(0, SellerPriceMessages.Length)];
            case BargainState.Bargain:
                return BargainMessages[Random.Range(0, BargainMessages.Length)];
            case BargainState.Accept:
                return AcceptMessages[Random.Range(0, AcceptMessages.Length)];
            case BargainState.Reject:
                return RejectMessages[Random.Range(0, RejectMessages.Length)];
            default:
                return "Invalid message state.";
        }
    }
}
