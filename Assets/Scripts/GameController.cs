using Unity.VisualScripting;
using UnityEngine;

public enum gameState { freeRoam, dialogue, battle }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    gameState state;

    //switches the player control state when interacting with an NPC
    private void Start()
    {
        DialogueManager.Instance.onShowDialogue += () =>
        {
            state = gameState.dialogue;
        };
        DialogueManager.Instance.onHideDialogue += () =>
        {
            if (state == gameState.dialogue) state = gameState.freeRoam;
        };
    }

    //switches player controls depending on the active game state
    private void Update()
    {
        if (state == gameState.freeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == gameState.dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
        else if (state == gameState.battle)
        {

        }
    }
}
