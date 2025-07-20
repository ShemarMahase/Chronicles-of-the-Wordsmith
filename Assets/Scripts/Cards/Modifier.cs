using UnityEngine;

[CreateAssetMenu(fileName = "Modifier", menuName = "Scriptable Objects/Modifier")]
public abstract class Modifier : ScriptableObject
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
