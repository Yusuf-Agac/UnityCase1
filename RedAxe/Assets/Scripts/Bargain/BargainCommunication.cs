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
        Reject,
        Angry
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
        "Pleasure doing business with you.",
        "Okay, I'll take it.",
        "Okay, I'll buy it."
    };
    
    private static readonly string[] RejectMessages = new string[]
    {
        "No, it's too low.",
        "I can't sell it for this price.",
        "Can you give me a better price?",
        "I can't go any lower.",
        "What are you talking about?",
        "OMG are you kidding me?",
        "I'm calling the police, you are crazy."
    };
    
    private static readonly string[] AngryMessages = new string[]
    {
        "I'm not gonna sell it to you.",
        "I'm not gonna sell it to you, get out of here.",
        "Get out of here.",
        "I'm not gonna sell it to you, you are wasting my time.",
        "Get out of here, you are wasting my time."
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
            case BargainState.Angry:
                return AngryMessages[Random.Range(0, AngryMessages.Length)];
            default:
                return "Invalid message state.";
        }
    }
}
