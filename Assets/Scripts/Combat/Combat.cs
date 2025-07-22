using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    protected StatSheet stats;
    string Combatname;

    private void Start()
    {
        stats = GetComponent<StatSheet>();
        TurnManager.defend += Defend;
    }

    public void SetName(string s)
    {
        Debug.Log("setting " + s + "as name");
        Combatname = s;
    }

    public string GetName()
    {
        return Combatname;
    }
    public void Attack(Combat self, float dmg)
    {
        TurnManager.instance.LaunchAttack(self, dmg);
    }
    //sends self to turnManager to keep track of combatants
    public void SendSelf(Combat self)
    {
        TurnManager.instance.AddCombatant(self);
        setCheck(self, true);
    }

    public void setCheck(Combat self, bool check)
    {
        TurnManager.instance.setCheck(self, check);
    }

    void Defend(Combat attacker, float damage)
    {
        if (attacker.GetName() == GetName())
        {
            float health = stats.GetStat(TurnManager.Stat.Health);
            float defense = stats.GetStat(TurnManager.Stat.Defense);
            health -= (damage - defense);
            if (health <= 0) Debug.Log("I the Enemy, am dead.");
            else Debug.Log("Me the Enemy took " + (damage - defense) + " damage, and now have " + health + " health left");
            stats.SetStat(TurnManager.Stat.Health, (int)health);
            TurnManager.instance.setCheck(this, true);
        }
    }

}
