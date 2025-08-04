using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ListeningParry : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI text;
    public float currentTime = 0f;
    FuzzyString fuzzy = new FuzzyString();
    float maxDamageReduction = .5f;
    bool isDone = false;
    float damageReduction = .5f;
    // Allows player to start typing for an allotted time
    public IEnumerator StartTyping(float timeLimit)
    {
        isDone = false;
        currentTime = 0;
        text.text = "";
        while (currentTime < timeLimit && !isDone)
        {
            CheckCharacters();
            yield return null;
            currentTime += Time.deltaTime;
        }
        ValidateAnswer();
    }
    //Iterates through each character typed in a frame and updates the on screen text
    private void CheckCharacters()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                string currentText = text.text;
                if (currentText.Length == 1) text.text = ""; 
                if (currentText.Length > 0) text.text = currentText.Substring(0, currentText.Length - 1);
            }
            else if ((c == '\n') || (c == '\r') || text.text == card.text) // enter/return
            {
                isDone = true;
                break;
            }
            else
            {
                text.text += c;
            }
        }
    }
    //Gets score for how close the string was and reduces damage proportional to it
    void ValidateAnswer()
    {
        float score = fuzzy.GetFuzzyCost(text.text, card.text);
        int max = Mathf.Max(text.text.Length, card.text.Length);
        damageReduction = ((max - score) / max) * maxDamageReduction;
        Debug.Log("Damage reduction is " + damageReduction);
        isDone = true;
    }
    public void SetCard(Card card) { this.card = card; }

    public float GetDamageReduction() { return damageReduction; }
}
