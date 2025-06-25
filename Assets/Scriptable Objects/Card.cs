using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public abstract class Card : ScriptableObject
{
    enum cardType
    {
        Translation,
        MultipleChoice,
        Matching
    }

    public string text; // What you want the card to say
    public string[] answers; //Every card will have an "expected answer", multiple choice will have multiple options, answers[0] will always be the correct answer
    public Buff buff; //the buff this card has, if any


}
