using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCombat : Combat
{
    private Deck deck = new Deck();
    private Card[] hand = new Card[10];
    int handSize = 3;
    [SerializeField] HandVisualizer handVisual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TurnManager.playerTurn += startTurn;
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.playerAttack += LaunchAttack;
    }

    private void LaunchAttack(bool fullDamage)
    {
        float damage = stats.GetStat(TurnManager.Stat.Attack);
        if(!fullDamage) damage /= 2;
        TurnManager.instance.LaunchAttack(this, damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeSelf(object sender, System.EventArgs e)
    {
        Debug.Log("its making it this far right?");
        SetName("Player");
        SendSelf(this);
        deck.InitializeDeck();
    }
    void startTurn(object sender, System.EventArgs e)
    {
        deck.getNextHand(hand, handSize);
        handVisual.visualizeHand(hand, handSize);
    }


}
