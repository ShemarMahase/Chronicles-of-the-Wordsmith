using UnityEngine;

[CreateAssetMenu(fileName = "MultipleChoice", menuName = "Scriptable Objects/Cards/MultipleChoice")]
public class MultipleChoice : Card
{
    MultipleChoice()
    {
        this.type = Card.cardType.MultipleChoice;
    }
    public string[] choices;
    public int answerIDX;

    private void OnEnable()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            audioText[i] = choices[i];
        }
    }
}
