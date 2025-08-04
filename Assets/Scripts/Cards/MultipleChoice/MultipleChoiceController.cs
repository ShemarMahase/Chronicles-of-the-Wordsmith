using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public IEnumerator StartMultipleChoice()
    {
        EnableButtons();
        isDone = false;
        currentTime = 0;
        while (currentTime < timeLimit && !isDone)
        {

            yield return null;
            currentTime+= Time.deltaTime;
        }
        yield return new WaitForSeconds(1f); 
        bool isCorrect = (card.choices[card.answerIDX] == selectedAnswer);
        TurnManager.instance.TriggerPlayerAttack(true);
        UIManager.instance.DisableMultipleChoice();
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
        DisableButtons();
        isDone = true;

    }

    private void DisableButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if(i == card.answerIDX)
            {
                continue;
            }
            buttonList[i].gameObject.SetActive(false);
        }
    }

    private void EnableButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(true);
        }
    }
}
