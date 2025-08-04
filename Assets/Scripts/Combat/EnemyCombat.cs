using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCombat : Combat
{
    List<Card> cards = new List<Card>();
    ListeningParry LP;
    float attackTime = 3f;
    void Awake()
    {
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.enemyTurn += PlayTurn;
        startPos = new Vector2(5.64f, -0.51f);
        StrikePos = new Vector2(-5.05f, -.29f);
        GetAllTranslationCards();
        LP = GameObject.FindWithTag("Listening Comprehension").GetComponent<ListeningParry>();
        LP.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetAllTranslationCards()
    {
        foreach (Card card in CardManager.Instance.CardCollection) {
            if (card.getCardType() == Card.cardType.Translation) cards.Add(card);
        }
    }
    void InitializeSelf(object sender, System.EventArgs e)
    {
        SetName("Enemy");
        SendSelf(this);
    }

    void PlayTurn(object sender, EventArgs e)
    {
        StartCoroutine(EnemyAttackSequence());
    }

    IEnumerator EnemyAttackSequence()
    {
        yield return StartCoroutine(TriggerEffectTicks());
        yield return new WaitForSeconds(1f);
        StartCoroutine(StartParry());
        Debug.Log("Starting attack animation");
        yield return StartCoroutine(AttackAnimation());
    }

    IEnumerator StartParry()
    {
        int rand = UnityEngine.Random.Range(0, cards.Count);
        AudioManager.instance.PlayAudio(cards[rand].audioClip);
        UIManager.instance.EnablistenTask();
        Debug.Log(LP);
        Debug.Log("Setting card");
        LP.SetCard(cards[rand]);
        attackTime = cards[rand].audioClip.length + 2f;
        yield return StartCoroutine(LP.StartTyping(attackTime));
    }

    void DealDamage()
    {
        Attack(this, stats.GetStat(TurnManager.Stat.Attack));
    }

    public IEnumerator AttackAnimation()
    {
        Debug.Log("this is where a parry thing would start");
        yield return Move(startPos, StrikePos,attackTime);
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(1);
        yield return Move(StrikePos, startPos, 1.5f);
        TurnManager.instance.setCheck(this, true);
    }

}
