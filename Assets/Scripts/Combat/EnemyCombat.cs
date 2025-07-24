using System;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCombat : Combat
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        TurnManager.initializeSelf += InitializeSelf;
        TurnManager.enemyTurn += PlayTurn;
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
        AttackAnimation();

    }

    public void AttackAnimation()
    {
        Attack(this, stats.GetStat(TurnManager.Stat.Attack));
    }

}
