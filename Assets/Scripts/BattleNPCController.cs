using System.Collections;
using UnityEngine;

public class BattleNPCController : MonoBehaviour, Interactable
{
    //NPC for initiating battle(WIP)
    [SerializeField] Dialogue dialogue;
    public void Interact()
    {
        Debug.Log("This is a Battle NPC");
        StartCoroutine(Convo());
    }

    IEnumerator Convo()
    {
        yield return StartCoroutine(DialogueManager.Instance.showDialogue(dialogue));
        yield return new WaitForSeconds(1.5f);
        GameSceneManager.Instance.LoadBattle();
    }
}
