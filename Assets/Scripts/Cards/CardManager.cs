using System.Collections.Generic;
using UnityEngine;

public class CardManager: MonoBehaviour
{
    public static CardManager Instance { get; private set; }
    public List<Card> PlayerDeck { get; private set; }
    [SerializeField] public List<Card> CardCollection;

    //Initialize cards
    void Awake()
    {
        Instance = this;
        PlayerDeck = new List<Card>();
        PlayerDeck.Add(CardCollection[0]);
        PlayerDeck.Add(CardCollection[1]);
        PlayerDeck.Add(CardCollection[2]);
        PlayerDeck.Add(CardCollection[3]);
        PlayerDeck.Add(CardCollection[4]);
    }
    //Lets the player retrieve current cards
    public List<Card> GetPlayerCards()
    {
        return PlayerDeck;
    }
}
