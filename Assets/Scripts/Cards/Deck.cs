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
    //Shuffles deck in reverse order using Fisher-Yates algorithm, until a stopping point.
    public void shuffleDeck(int end = 0)// The stopping point (exclusive)
    {
        for (int i = cards.Count - 1; i > end; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        currentCard = 0;
    }
    // Swaps elements at the end of a deck to the front of a deck then performs a shuffle ignoring front elements
    public void SwapAndShuffle(int idx)
    {
        int j = 0;
        for (int i = idx; i < cards.Count; i++)
        {
            (cards[i], cards[j]) = (cards[j], cards[i]);
            j++;
        }
        shuffleDeck(j);
    }
    //Gives the player their next hand, shuffling if necessary, and applying any modifiers if applicable
    public void getNextHand(Card[] hand, int drawAmount,Stance stance)
    {
        int tempCount = currentCard;
        Debug.Log($"draw amount is {drawAmount}, current card index is at {currentCard}");
        Debug.Log($"there are {cards.Count} in the deck");
        if (currentCard + drawAmount >= cards.Count) //If cards left in deck arent enough for a full hand, add cards then shuffle
        {
            Debug.Log("not enough cards in deck, shuffling");
            SwapAndShuffle(currentCard); // Takes the remaining unused cards and puts them at the front of the deck and shuffles the remaining cards
            tempCount = 0;
        }
        int j = 0;
        for (int i = tempCount; i < tempCount+drawAmount; i++) 
        {
            Modifier mod = stance.GetMod();
            Debug.Log("Selected mod is " + mod);
            Debug.Log($"Card at index {i} is {cards[i].name}");
            hand[j] = cards[i];
            hand[j].SetMod(mod);
            j++;
        }
        currentCard += drawAmount;

    }
    //look ahead at the next "amount" cards
    public void Scry(int amount) { }
}
