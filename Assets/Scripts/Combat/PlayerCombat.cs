using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Combat
{
    private Deck deck = new Deck();
    private Card[] hand = new Card[10];
    int handSize = 3;
    [SerializeField] HandVisualizer handVisual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnManager.playerTurn += startTurn;
        TurnManager.initializeSelf += InitializeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeSelf(object sender, System.EventArgs e)
    {
        deck.InitializeDeck();
        deck.getNextHand(hand, handSize);
        handVisual.visualizeHand(hand, handSize);
    }
    void startTurn(object sender, System.EventArgs e)
    {
        deck.getNextHand(hand, handSize);
        handVisual.visualizeHand(hand, handSize);
    }


}
