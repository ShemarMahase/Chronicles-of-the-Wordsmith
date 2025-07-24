using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerCombat : Combat
{
    private Deck deck = new Deck();
    private Card[] hand = new Card[10];
    bool fullDamage = false;
    int handSize = 3;
    [SerializeField] HandVisualizer handVisual;
    Vector2 starPos = new Vector2(-6.36f, -.29f);
    Vector2 StrikePos = new Vector2(5.09f, -.29f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TurnManager.playerTurn += startTurn;
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.playerAttack += LaunchAttack;
    }

    private void LaunchAttack(bool complete)
    {
        fullDamage = complete;
        StartCoroutine(AttackAnimation());
    }
    IEnumerator AttackAnimation()
    {
        yield return Move(starPos, StrikePos);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        yield return Move(StrikePos, starPos);
        Debug.Log("Player is Ready");
        TurnManager.instance.setCheck(this, true);
    }
    public void DealDamage()
    {
        float damage = stats.GetStat(TurnManager.Stat.Attack);
        if (!fullDamage) damage /= 2;
        TurnManager.instance.LaunchAttack(this, damage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeSelf(object sender, System.EventArgs e)
    {
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
