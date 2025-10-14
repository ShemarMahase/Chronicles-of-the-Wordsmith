using System;
using UnityEngine;

public class MultipleChoiceLogic : CardLogic
{
    MultipleChoiceController Mcc;
    //When Multiple choice card is selected, enable multiple choice mini game
    public void Onclick()
    {
        UIManager.instance.EnableMultipleChoice();
        Mcc.SetCard(card);
        Mcc.StartCoroutine(Mcc.StartMultipleChoice());
        UIManager.instance.DisableCards();
    }

    public void SetController(MultipleChoiceController multipleChoiceController)
    {
        Mcc = multipleChoiceController;
    }
}
