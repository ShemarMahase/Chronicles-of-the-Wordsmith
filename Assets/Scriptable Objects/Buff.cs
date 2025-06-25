using UnityEngine;

[CreateAssetMenu(fileName = "Buff", menuName = "Scriptable Objects/Buff")]
public abstract class Buff : ScriptableObject
{
    enum ModifierType
    {
        Dot,
        Heal,
        Buff,
        Debuff,
        Scry,
        ExtraTurn
    }
    public int value;
    public bool isPercent;
    TurnManager.Stat stat;
    int duration;


    public virtual void onApply()
    {
        Debug.Log("hello");
    }
    public virtual void onTick()
    {
        Debug.Log("hello");
    }
    public virtual void onRemove()
    {
        Debug.Log("hello");
    }
}
