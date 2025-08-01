using UnityEngine;

[CreateAssetMenu(fileName = "Matching", menuName = "Scriptable Objects/Cards/Matching")]
public class Matching : Card
{
    Matching()
    {
        this.type = Card.cardType.Matching;
    }
    public StringStringDictionary matchingPairs = new StringStringDictionary();
}
