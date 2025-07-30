using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageOverTime", menuName = "Scriptable Objects/Modifiers/DamageOverTime")]
public class DamageOverTime : Modifier
{
    public override void onApply(Combat combatant)
    {

    }

    public override void onRemove(Combat combatant)
    {

    }

    public override void onTick(Combat combatant)
    {
        combatant.Defend(combatant, value);
    }
}
