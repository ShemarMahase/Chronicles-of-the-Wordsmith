using UnityEngine;

[CreateAssetMenu(fileName = "Translation", menuName = "Scriptable Objects/Translation")]
public class Translation : Card
{
    Translation()
    {
        this.type = Card.cardType.Translation;
    }
    public string answer;
}
