using UnityEngine;
using UnityEngine.UI;

public class HandVisualizer : MonoBehaviour
{
    public GameObject TranslationCard;
    public GameObject MatchingCard;
    public GameObject MultipleChoiceCard;
    public RectTransform cardArea;

    //Instantiates each card
    public void visualizeHand(Card[] cards, int handsize)
    {
        for(int i = 0; i < handsize; i++)
        {
            CreateCard(cards[i]);
        }
    }
    //Matches incoming card and instantiates the corresponding prefab
    private void CreateCard(Card card)
    {
        GameObject gameObj = null;
        Debug.Log(card);
        switch (card.getCardType())
        {
            case Card.cardType.Translation:
                gameObj = Instantiate(TranslationCard, cardArea);
                break;
            case Card.cardType.MultipleChoice:
                gameObj = Instantiate(MultipleChoiceCard, cardArea);
                break;
            case Card.cardType.Matching:
                gameObj = Instantiate(MatchingCard, cardArea);
                break;
        }
        CardLogic visual = gameObj.GetComponent<CardLogic>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(cardArea);
        visual.setCard(card);
    }
}
