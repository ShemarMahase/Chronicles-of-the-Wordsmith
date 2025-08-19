using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    List<Card> cards;
    int currentCard;
    //Adds cards to this Deck, from player collection of cards
    public void InitializeDeck()
    {
        cards = CardManager.Instance.GetPlayerCards();
        shuffleDeck();
    }
    //intitializes this deck with stance cards
    public void InitializeDeck(List<Card> stances)
    {
        for(int i = 0; i < stances.Count; i++)
        {
            (stances[i] as Stance).NewStance();
        }
        cards = stances;
        shuffleDeck();
    }
    //shuffles deck
    public void shuffleDeck() 
    {
        for (int i = cards.Count-1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        currentCard = 0;
    }
    //Gives the player their next hand, applying any modifiers if applicable
    public void getNextHand(Card[] hand, int drawAmount,Stance stance)
    {
        int tempCount = 0;
        Debug.Log(drawAmount);
        if (currentCard + drawAmount >= cards.Count) //If cards left in deck arent enough for a full hand, add cards then shuffle
        {
            for (tempCount = 0; tempCount < cards.Count; tempCount++) hand[tempCount] = cards[tempCount];
            shuffleDeck();
        }
        for (int i = tempCount; i < drawAmount; i++) 
        {
            Modifier mod = stance.GetMod();
            Debug.Log("Selected mod is "+mod);
            hand[i] = cards[i];
            hand[i].SetMod(mod);
        }
            currentCard += drawAmount;

    }
    //look ahead at the next "amount" cards
    public void Scry(int amount) { }
}
