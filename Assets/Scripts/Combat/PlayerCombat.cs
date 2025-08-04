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
    private Deck stances = new Deck();
    private Card[] hand = new Card[10];
    bool fullDamage = false;
    int handSize = 3;
    [SerializeField] HandVisualizer handVisual;
    [SerializeField] Stance stance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        TurnManager.playerTurn += startTurn;
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.playerAttack += LaunchAttack;
        TurnManager.initiateShuffle += ChangeStance;
        TurnManager.setStance += SetStance;
        TurnManager.triggerPlayerEffects += TriggerPlayerEffects;
        TurnManager.setPlayerMod += SetMod;
        startPos = new Vector2(-6.36f, -.29f);
        StrikePos = new Vector2(5.09f, -.29f);
    }

    //Starts attack sequence
    private void LaunchAttack(bool complete)
    {
        fullDamage = complete;
        StartCoroutine(AttackAnimation());
    }
    private void TriggerPlayerEffects()
    {
        StartCoroutine(TriggerEffectTicks());
    }
    //Controls walk to enemy, attacking, and walking back
    IEnumerator AttackAnimation()
    {
        anim.SetFloat("moveX", 1);
        anim.SetFloat("moveY", 0);
        yield return Move(startPos, StrikePos, 1.5f);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        anim.SetFloat("moveX", -1);
        yield return Move(StrikePos, startPos, 1.5f);
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
        if (pendingMod?.GetTarget() == Modifier.EffectTarget.Self)
        {
            pendingMod.onApply(this);
            pendingMod = null;

        }
        TurnManager.instance.LaunchAttack(this, damage, pendingMod);
    }

    //Initializes name and deck, and sends self to turn manager
    void InitializeSelf(object sender, System.EventArgs e)
    {
        SetName("Player");
        SendSelf(this);
        deck.InitializeDeck();
        stances.InitializeDeck(CardManager.Instance.GetStanceCards());

    }
    //Starts a new turn
    void startTurn(object sender, System.EventArgs e)
    {
        deck.getNextHand(hand, handSize, stance);
        handVisual.visualizeHand(hand, handSize);
    }
    //Changes players current stance
    public void ChangeStance()
    {
        Debug.Log(hand);
        stances.getNextHand(hand, handSize, stance);
        handVisual.visualizeHand(hand, handSize);
    }

    public void SetStance(Stance stance)
    {
        this.stance = stance;
        UIManager.instance.DisableCards();
        Debug.Log("My stance is now " + this.stance.text);
        setCheck(this, true);
    }

    public void SetMod(Modifier mod)
    {
        pendingMod = mod;
        Debug.Log("player mod got set to " + pendingMod);
    }


}
