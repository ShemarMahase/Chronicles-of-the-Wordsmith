using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceController : MonoBehaviour
{
    [SerializeField] List<Button> buttonList;
    [SerializeField] TextMeshProUGUI query;
    MultipleChoice card;
    string selectedAnswer;
    bool isDone = false;
    int timeLimit = 15;
    float currentTime;
    public void SetCard(Card card)
    {
        this.card = (card as MultipleChoice);
        SetButtons();
        query.text = card.text;
    }

    IEnumerator StartMultipleChoice()
    {
        isDone = false;
        currentTime = 0;
        while (currentTime < timeLimit && !isDone)
        {

            yield return null;
            currentTime+= Time.deltaTime;
        }
        bool isCorrect = (card.choices[card.answerIDX] == selectedAnswer);
        TurnManager.instance.TriggerPlayerAttack(true);
    }

    private void SetButtons()
    {
        int i = 0;
        foreach (var button in buttonList) { 
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.choices[i];
            i++;
        }
    }

    public void Onclick(int answer)
    {
        switch (answer)
        {
            case 0:
                selectedAnswer = card.choices[0];
                break;
            case 1:
                selectedAnswer = card.choices[1];
                break;
            case 2:
                selectedAnswer = card.choices[2];
                break;
            case 3:
                selectedAnswer = card.choices[3];
                break;
        }

    }
}
