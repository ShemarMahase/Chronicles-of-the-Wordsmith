using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Modifier")]
public abstract class Modifier : ScriptableObject
{
    //Enum of all possible Modifiers
    public enum ModifierType
    {
        Dot,
        Heal,
        Buff,
        Debuff,
        Scry,
        ExtraTurn
    }
    [SerializeField] protected int value; // value of the modifier, (how much DOT damage, damage increase,number of cards scryed, etc)
    [SerializeField] protected bool isPercent; // Is the value a flat number or percentage
    [SerializeField] protected TurnManager.Stat stat; //The modifier this uses
    [SerializeField] protected int duration; // Total duration a modifer will last
    [SerializeField] protected int weight; // How rare this Modifier should be, higher the weight the more common
    [SerializeField] Sprite image; //The sprite the mod uses on cards/under the player
    [SerializeField] string ModInfo; // When hovering over a mod, the text that will be displayed to a player

    // Triggers when buff is first applied
    public abstract void onApply(Combat combatant);
    //triggers every turn
    public abstract void onTick(Combat combatant);
    //triggers when the buff is removed
    public abstract void onRemove(Combat combatant);
    public int GetDuration() {  return duration; }
    public int GetWeight() { return weight; }

    public Sprite GetSprite() { return  image; }
    public string GetInfo() {  return ModInfo; }
}
