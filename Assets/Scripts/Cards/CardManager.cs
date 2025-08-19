using System.Collections.Generic;
using UnityEngine;

public class CardManager: MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    public List<Card> PlayerDeck { get; private set; }
    [SerializeField] public AllCards CardCollection;
    [SerializeField] public List<Card> StanceCollection;

    //Initialize cards
    void Awake()
    {
        Instance = this;
        PlayerDeck = new List<Card>();
        foreach(Card card in CardCollection.cards)
        {
            PlayerDeck.Add(card);
        }
    }
    //Lets the player retrieve current cards
    public List<Card> GetPlayerCards()
    {
        return PlayerDeck;
    }

    public List<Card> GetStanceCards()
    {
        return StanceCollection;
    }
}
