using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class Combat : MonoBehaviour
{
    protected StatSheet stats;
    string Combatname;
    protected Animator anim;

    private void Start()
    {
        stats = GetComponent<StatSheet>();
        TurnManager.defend += Defend;
        anim= GetComponent<Animator>();
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
    public IEnumerator Move(Vector2 start, Vector2 end)
    {
        anim.SetBool("isMoving", true);
        float totalMovementTime = 3f;
        float currentTime = 0;
        while (Vector2.Distance(transform.position, end) > 0)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(start, end, currentTime / totalMovementTime);
            yield return null;
        }
        anim.SetBool("isMoving", false);
    }
    void Defend(Combat attacker, float damage)
    {
        if (attacker.GetName() == GetName())
        {
            anim.SetTrigger("Hurt");
            float health = stats.GetStat(TurnManager.Stat.Health);
            float defense = stats.GetStat(TurnManager.Stat.Defense);
            health -= (damage - defense);
            if (health <= 0) Debug.Log("I "+GetName()+ " am dead.");
            else Debug.Log(GetName()+" took " + (damage - defense) + " damage, and now have " + health + " health left");
            stats.SetStat(TurnManager.Stat.Health, (int)health);
            Debug.Log(GetName()+" is Ready");
            TurnManager.instance.setCheck(this, true);
        }
    }

}
