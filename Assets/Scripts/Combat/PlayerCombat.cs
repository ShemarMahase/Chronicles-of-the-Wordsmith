using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerCombat : Combat
{
    private Deck deck = new Deck();
    private Card[] hand = new Card[10];
    bool fullDamage = false;
    int handSize = 3;
    [SerializeField] HandVisualizer handVisual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TurnManager.playerTurn += startTurn;
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.playerAttack += LaunchAttack;
        startPos = new Vector2(-6.36f, -.29f);
        StrikePos = new Vector2(5.09f, -.29f);
    }

    //Starts attack sequence
    private void LaunchAttack(bool complete)
    {
        fullDamage = complete;
        StartCoroutine(AttackAnimation());
    }
    //Controls walk to enemy, attacking, and walking back
    IEnumerator AttackAnimation()
    {
        anim.SetFloat("moveX", 1);
        anim.SetFloat("moveY", 0);
        yield return Move(startPos, StrikePos);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        anim.SetFloat("moveX", -1);
        yield return Move(StrikePos, startPos);
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);
        Debug.Log("Player is Ready");
        TurnManager.instance.setCheck(this, true);
    }
    //animation event, deals damage on impact frame
    public void DealDamage()
    {
        Debug.Log("damage dealt");
        float damage = stats.GetStat(TurnManager.Stat.Attack);
        if (!fullDamage) damage /= 2;
        TurnManager.instance.LaunchAttack(this, damage);
    }

    //Initializes name and deck, and sends self to turn manager
    void InitializeSelf(object sender, System.EventArgs e)
    {
        SetName("Player");
        SendSelf(this);
        deck.InitializeDeck();
    }
    //Starts a new turn
    void startTurn(object sender, System.EventArgs e)
    {
        deck.getNextHand(hand, handSize);
        handVisual.visualizeHand(hand, handSize);
    }


}
