using UnityEngine;

public class ActionButtons : MonoBehaviour
{
    // Tells turnmanger player is going to use an action
    public void Act()
    {
        TurnManager.instance.PlayerAction();
    }
    // Tells turnmanger player is going to shuffle
    public void Shuffle()
    {
        TurnManager.instance.ShufflePlayer();
    }
    // Tells turnmanger player is going to use an item
    public void Item()
    {
        TurnManager.instance.UseItem();
    }
}
