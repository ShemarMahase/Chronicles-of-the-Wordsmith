using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stance", menuName = "Scriptable Objects/Stance")]
public class Stance : ScriptableObject
{
    [SerializeField] protected List<Modifier> modifiers; 
    public int baseHealth = 0;
    public int baseAttack = 0;
    public int baseDefense = 0;




}
