using UnityEngine;

[CreateAssetMenu(fileName = "Translation", menuName = "Scriptable Objects/Cards/Translation")]
public class Translation : Card
{
    Translation()
    {
        this.type = Card.cardType.Translation;
    }
    public string correctTranslation;

    public string GetCorrectTranslation()
    {
        return correctTranslation;
    }
}
