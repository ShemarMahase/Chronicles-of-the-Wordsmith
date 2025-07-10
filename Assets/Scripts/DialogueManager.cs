using System;
using System.Collections;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;

    [SerializeField] int lettersPerSecond;

    //events to tell the program when to start and end dialogue
    public event Action onShowDialogue;
    public event Action onHideDialogue;

    public static DialogueManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    int currentLine = 0;
    bool isTyping;

    //shows the dialogue box on screen and begins the interaction
    public IEnumerator showDialogue(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();
        onShowDialogue?.Invoke();

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(typeDialogue(dialogue.Lines[0]));
    }

    //when in the dialogue state, moves to next line when J is pressed
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(typeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                dialogueBox.SetActive(false);
                onHideDialogue.Invoke();
                currentLine = 0;
            }
        }
    }

    //function to type out dialogue letter by letter
    public IEnumerator typeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
