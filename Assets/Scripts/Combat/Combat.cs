using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public abstract class Combat : MonoBehaviour
{
    [SerializeField] public StatSheet stats;
    [SerializeField] protected StatSheet modifiedStats;
    protected Modifier pendingMod;
    string Combatname;
    protected Animator anim;

    public Slider playerHealthBar;
    public GameObject damageNumberPrefab;
    protected Vector2 startPos;
    protected Vector2 StrikePos;
    protected bool effectChange = false;
    [System.Serializable]
    public struct ActiveEffect
    {
        public Modifier effect;
        public float duration;

        public ActiveEffect(Modifier mod)
        {
            effect = mod;
            duration = mod.GetDuration();
        }

        public void decreaseDuration(int i)
        {
            duration -= i;
        }
    }

    public List<ActiveEffect> activeEffects = new List<ActiveEffect>();

    //initialized animator and healthbar
    protected void Start()
    {
        TurnManager.defend += Defend;

        if (playerHealthBar != null)
        {
            playerHealthBar.maxValue = stats.GetStat(TurnManager.Stat.MaxHealth);
            playerHealthBar.value = stats.GetStat(TurnManager.Stat.Health);
        }
        anim = GetComponent<Animator>();
    }

    //Updates player health bar
    public void UpdatePlayerHealth(float newHealth)
    {
        if (playerHealthBar != null)
            playerHealthBar.value = newHealth;
    }
    //Sets name of combatant
    public void SetName(string s)
    {
        Debug.Log("setting " + s + "as name");
        Combatname = s;
    }
    //Gets the name of the Combatant
    public string GetName()
    {
        return Combatname;
    }
    //Sends attack to the turnManager
    public void Attack(Combat self, float dmg)
    {
        TurnManager.instance.LaunchAttack(self, dmg, pendingMod);
    }
    //sends self to turnManager to keep track of combatants
    public void SendSelf(Combat self)
    {
        TurnManager.instance.AddCombatant(self);
        setCheck(self, true);
    }
    // Tells TurnManager this combatants action is done.
    public void setCheck(Combat self, bool check)
    {
        TurnManager.instance.setCheck(self, check);
    }
    //Moves Combatant from start position to end position
    public IEnumerator Move(Vector2 start, Vector2 end)
    {
        anim.SetBool("isMoving", true);
        float totalMovementTime = 1.5f;
        float currentTime = 0;
        while (Vector2.Distance(transform.position, end) > 0)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector2.Lerp(start, end, currentTime / totalMovementTime);
            yield return null;
        }
        anim.SetBool("isMoving", false);
    }
    //Called when turn Manager sends damage. Decreases health based on defence, and damage effect
    public void Defend(Combat attacker, float damage, Modifier mod)
    {
        Debug.Log("Attacker is " + attacker.GetName());
        if (attacker.GetName() == GetName())
        {
            anim.SetTrigger("Hurt");

            //calculate how much damage is taken
            TakeDmg(damage);

            //applies any mods if present
            if (mod) mod?.onApply(this);
            TurnManager.instance.setCheck(this, true);
        }
    }

    //Visual Effect for damage
    public void SpawnDamageNum(float damageTaken)
    {
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
    }

    // Defaults stat values to 0 and adds up any buffs/debuffs, adding it to the modified stats Sheet
    public void RecalculateEffects()
    {
        modifiedStats.Clear();
        for (int i = 0; i < activeEffects.Count; i++)
        {
            Modifier effect = activeEffects[i].effect;
            float stat = modifiedStats.GetStat(effect.GetStat()); //gets current tally for selected stat
            if (effect.IsPercent()) stat += (effect.GetValue() * stats.GetStat(effect.GetStat())); //if percentage buff multiply by value
            else stat += effect.GetValue(); // otherwise just add it directly
            modifiedStats.SetStat(effect.GetStat(), (int)stat); //add new calculation back to statSheet
        }
    }
    //Trigger every active effect
    public void TriggerEffectTicks()
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].effect.onTick(this);
            activeEffects[i].decreaseDuration(-1);
            if (activeEffects[i].duration <= 0)
            {
                activeEffects[i].effect.onRemove(this);
                activeEffects.RemoveAt(i);
                RecalculateEffects();
            }

        }
    }

    //Deals Damage to Combatant depending on if damage defense ignoring or not
    public void TakeDmg(float dmg, bool ignoreDefence = false)
    {
        float damageTaken = dmg;
        if (!ignoreDefence)
        {
            float defense = stats.GetStat(TurnManager.Stat.Defense);
            damageTaken = Math.Max(0f, dmg - defense);
        }
        SetHealth(damageTaken);
    }

    //Modifies health, if Combatant is at 0 or less hp trigger battle end
    public void SetHealth(float dmg)
    {
        float health = stats.GetStat(TurnManager.Stat.Health);
        Debug.Log("taking " + dmg + " damage");
        health -= dmg;
        Debug.Log("Health is now " + health + " HP");
        SpawnDamageNum(dmg);
        if (health <= 0f)
        {
            Debug.Log("Player lost battle");
            return;
        }
        stats.SetStat(TurnManager.Stat.Health, (int)health);
        if (playerHealthBar != null && GetName() == "Player")
        {
            Debug.Log("updating health");
            UpdatePlayerHealth(health);
        }
    }
}
