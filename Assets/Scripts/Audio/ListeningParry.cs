using System.Collections;
using TMPro;
using UnityEngine;

public class ListeningParry : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI text;
    public float currentTime = 0f;
    // Allows player to start typing 
    public IEnumerator StartTyping(float timeLimit)
    {
        currentTime = 0;
        text.text = "";
        Debug.Log("typing is starting");
        while (currentTime < timeLimit)
        {
            CheckCharacters();
            yield return null;
            currentTime += Time.deltaTime;
        }
        ValidateAnswer();
        UIManager.instance.DisableListenTask();
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
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                ValidateAnswer();
            }
            else
            {
                text.text += c;
            }
        }
    }
    void ValidateAnswer()
    {
        //do fuzzy string stuff
        if (text.text.Equals(card.text)) Debug.Log("wow");
        //UIManager.instance.DisableListenTask();
    }
    public void SetCard(Card card) { this.card = card; }
}
