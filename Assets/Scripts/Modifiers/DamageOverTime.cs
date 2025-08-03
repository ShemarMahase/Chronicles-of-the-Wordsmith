using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageOverTime", menuName = "Scriptable Objects/Modifiers/DamageOverTime")]
public class DamageOverTime : Modifier
{
    [SerializeField] bool defenseIgnore = false;
    public override void onApply(Combat combatant)
    {
        combatant.activeEffects.Add(new Combat.ActiveEffect(this));
    }

    public override void onRemove(Combat combatant)
    {

    }

    public override void onTick(Combat combatant)
    {
        float damageTaken = value;
        if (isPercent) damageTaken = combatant.stats.GetStat(stat) * (1/value);
        combatant.StartCoroutine(combatant.TakeDmg(damageTaken, defenseIgnore));
    }
}
