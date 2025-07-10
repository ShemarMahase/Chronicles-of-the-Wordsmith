using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogue dialogue;

    //Begins a dialogue sequence when interacted with
    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.showDialogue(dialogue));
    }
}
