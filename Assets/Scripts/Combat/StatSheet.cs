using System.Collections.Generic;
using UnityEngine;

public class StatSheet : MonoBehaviour
{
    Dictionary<TurnManager.Stat, int> stats = new Dictionary<TurnManager.Stat, int>();
    Stance stance;
    public int baseHealth = 10;
    public int baseAttack = 10;
    public int baseDefense = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InitializeStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStance(Stance stance)
    {
        this.stance = stance;
    }

    private float GetModifiedStat(TurnManager.Stat stat)
    {
        return stats[stat];
    }

    private void InitializeStats()
    {
        stats.Add(TurnManager.Stat.Health, baseHealth);
        stats.Add(TurnManager.Stat.MaxHealth, baseHealth);
        stats.Add(TurnManager.Stat.Attack, baseAttack);
        stats.Add(TurnManager.Stat.Defense, baseDefense);
    }

    public void ModifyStat(TurnManager.Stat stat, int amount)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat] += amount;
        }
    }

    public void SetStat(TurnManager.Stat stat, int amount)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat] = amount;
        }
    }

    public float GetStat(TurnManager.Stat stat)
    {
        if (stats.ContainsKey(stat)) return stats[stat];
        else return -1f;
    }
}
