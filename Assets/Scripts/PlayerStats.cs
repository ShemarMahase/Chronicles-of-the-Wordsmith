using UnityEngine;
using System.Collections.Generic;
public class PlayerStats : MonoBehaviour
{
    Dictionary<TurnManager.Stat, int> stats = new Dictionary<TurnManager.Stat, int>();
    int baseHealth = 10;
    int baseAttack = 10;
    int baseDefense = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeStats()
    {
        stats.Add(TurnManager.Stat.Health, baseHealth);
        stats.Add(TurnManager.Stat.MaxHealth, baseHealth);
        stats.Add(TurnManager.Stat.Attack, baseAttack);
        stats.Add(TurnManager.Stat.Defense, baseDefense);
    }

    public void ModifyStat(TurnManager.Stat stat,int amount)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat] += amount;
        }
    }

    public float GetStat(TurnManager.Stat stat)
    {
        if (stats.ContainsKey(stat)) return stats[stat];
        else return -1f;
    }
}
