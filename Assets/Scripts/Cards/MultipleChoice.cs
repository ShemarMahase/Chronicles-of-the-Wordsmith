using UnityEngine;

[CreateAssetMenu(fileName = "MultipleChoice", menuName = "Scriptable Objects/MultipleChoice")]
public class MultipleChoice : Card
{
    MultipleChoice()
    {
        this.type = Card.cardType.MultipleChoice;
    }
    public string[] choices;
    public string answer;
}
