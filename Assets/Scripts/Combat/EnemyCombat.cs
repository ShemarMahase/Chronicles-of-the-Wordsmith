using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCombat : Combat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.enemyTurn += PlayTurn;
        startPos = new Vector2(5.64f, -0.51f);
        StrikePos = new Vector2(-5.05f, -.29f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeSelf(object sender, System.EventArgs e)
    {
        SetName("Enemy");
        SendSelf(this);
    }

    void PlayTurn(object sender, EventArgs e)
    {
        StartCoroutine(AttackAnimation());
    }

    void DealDamage()
    {
        Attack(this, stats.GetStat(TurnManager.Stat.Attack));
    }

    public IEnumerator AttackAnimation()
    {
        Debug.Log("this is where a parry thing would start");
        yield return Move(startPos, StrikePos);
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(1);
        yield return Move(StrikePos, startPos);
        TurnManager.instance.setCheck(this, true);
    }

}
