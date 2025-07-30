using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public enum cardType
    {
        Translation,
        MultipleChoice,
        Matching,
        Stance
    }

    public cardType getCardType() { return type; }

    [SerializeField]
    protected cardType type;
    public string text; // What you want the card to say
    //public string[] answers; //Every card will have an "expected answer", multiple choice will have multiple options, answers[0] will always be the correct answer
    public Modifier mod; //the mod this card has, if any

    [SerializeField]
    private string correctTranslation;

    public string GetCorrectTranslation()
    {
        return correctTranslation;
    }

    public void SetMod(Modifier modifier)
    {
        mod = modifier;
    }
}
