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
        int tempCount = 0;
        Debug.Log(drawAmount);
        if (currentCard + drawAmount >= cards.Count)
        {
            for (tempCount = 0; tempCount < cards.Count; tempCount++) hand[tempCount] = cards[tempCount];
            shuffleDeck();
        }
        for (int i = tempCount; i < drawAmount; i++) hand[i] = cards[i];
        currentCard += drawAmount;

        }
    public void Scry(int amount) { }
}
