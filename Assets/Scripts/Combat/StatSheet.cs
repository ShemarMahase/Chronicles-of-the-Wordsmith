using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "StatSheet", menuName = "Scriptable Objects/StatSheet")]
public class StatSheet : ScriptableObject
{
    Dictionary<TurnManager.Stat, int> stats = new Dictionary<TurnManager.Stat, int>();
    [SerializeField] Stance stance;
    [SerializeField] int baseHealth = 0;
    [SerializeField] int baseAttack = 0;
    [SerializeField] int baseDefense = 0;
    
    //When Scriptable object is loaded, initialize stats
    void OnEnable()
    {
        if (stats.Count == 0)
        {
            InitializeStats();
        }
    }

    // Clears Stance
    public void ClearStance()
    {
        stance = null;
    }
    //Sets active stance, no stance is default of 0 values
    public void SetStance(Stance stance)
    {
        this.stance = stance;
    }
    //Gets what a stat would be after stance modifications
    private float GetStancedStat(TurnManager.Stat stat)
    {
        return stance ? (stats[stat] + stance.GetStat(stat)): stats[stat];
    }
    //Initializes stats by putting them into a dictionary
    private void InitializeStats()
    {
        stats.Add(TurnManager.Stat.Health, baseHealth);
        stats.Add(TurnManager.Stat.MaxHealth, baseHealth);
        stats.Add(TurnManager.Stat.Attack, baseAttack);
        stats.Add(TurnManager.Stat.Defense, baseDefense);
    }
    // Adds a modifier to a prexisting stat
    public void ModifyStat(TurnManager.Stat stat, int amount)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat] += amount;
        }
    }
    //Sets a stat directly to a value
    public void SetStat(TurnManager.Stat stat, int amount)
    {
        if (stats.ContainsKey(stat))
        {
            stats[stat] = amount;
        }
    }
    // Gets final stat with modifications
    public float GetStat(TurnManager.Stat stat)
    {
        if (stats.ContainsKey(stat)) return GetStancedStat(stat);
        else return -1f;
    }
}
