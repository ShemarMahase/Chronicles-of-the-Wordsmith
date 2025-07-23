using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public abstract class Combat : MonoBehaviour
{
    protected StatSheet stats;
    string Combatname;

    public Slider playerHealthBar;
    public GameObject damageNumberPrefab;


    private void Start()
    {
        stats = GetComponent<StatSheet>();
        TurnManager.defend += Defend;

        if (playerHealthBar != null)
        {
            playerHealthBar.maxValue = stats.GetStat(TurnManager.Stat.Health);
            playerHealthBar.value = stats.GetStat(TurnManager.Stat.Health);
        }
    }

    public void UpdatePlayerHealth(float newHealth)
    {
        if (playerHealthBar != null)
            playerHealthBar.value = newHealth;
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
            float damageTaken = Math.Max(0f,damage - defense);
            health -= damageTaken;
            
            stats.SetStat(TurnManager.Stat.Health, (int)health);

            if (playerHealthBar != null && GetName() == "Player") 
            {
                UpdatePlayerHealth(health);
            }

            // Spawn damage number
        if (damageNumberPrefab != null)
        {
            // Randomize position of damage number
            Vector3 baseOffset = new Vector3(2.5f, 0.5f, 0);
            float randomX = UnityEngine.Random.Range(-0.5f, 0.5f);
            float randomY = UnityEngine.Random.Range(-0.5f, 0.5f);

            // Convert world position to screen position for UI
            Vector3 spawnPos = transform.position + baseOffset + new Vector3(randomX, randomY, 0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(spawnPos);

            GameObject dmgNumObj = Instantiate(damageNumberPrefab, screenPos, Quaternion.identity, FindFirstObjectByType<Canvas>().transform);
            DamageNumber dmgNumScript = dmgNumObj.GetComponent<DamageNumber>();
            dmgNumScript.SetDamage(damageTaken);
        }

            if (health <= 0) Debug.Log("I the Enemy, am dead.");
            else Debug.Log("Me the Enemy took " + (damage - defense) + " damage, and now have " + health + " health left");

            TurnManager.instance.setCheck(this, true);
        }
    }

}
