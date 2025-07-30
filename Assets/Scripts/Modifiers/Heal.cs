using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Scriptable Objects/Modifiers/Heal")]
public class Heal : Modifier
{
    
    public override void onApply(Combat combatant)
    {
        //TurnManager.Heal(combatant);
    }

    public override void onRemove(Combat combatant)
    {
        //Debug.Log("removed");
    }

    public override void onTick(Combat combatant)
    {
        //TurnManager.Heal(combatant);
    }
}
