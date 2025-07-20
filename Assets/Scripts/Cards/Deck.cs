using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> cards;
    int currentCard;

    public void InitializeDeck()
    {
        cards = CardManager.Instance.GetPlayerCards();
        shuffleDeck();
    }
    public void shuffleDeck() 
    {
        for (int i = cards.Count-1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        currentCard = 0;
    }
    public void getNextHand(Card[] hand, int drawAmount)
    {
        Debug.Log(drawAmount);
        if (currentCard + drawAmount >= cards.Count) shuffleDeck();
        for (int i = 0; i < drawAmount; i++) hand[i] = cards[i];


        }
    public void Scry(int amount) { }
}
