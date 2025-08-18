using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllCards", menuName = "Scriptable Objects/AllCards")]
public class AllCards : ScriptableObject
{
    public List<Card> cards;
}
