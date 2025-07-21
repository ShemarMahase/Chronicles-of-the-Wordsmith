using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HandVisualizer : MonoBehaviour
{
    public TranslationInputUIController translationUIController;
    public GameObject TranslationCard;
    public GameObject MatchingCard;
    public GameObject MultipleChoiceCard;
    public RectTransform cardArea;

    private List<GameObject> currentCards = new List<GameObject>();

    //Instantiates each card
    public void visualizeHand(Card[] cards, int handsize)
    {

        for (int i = 0; i < handsize; i++)
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
                TranslationCardLogic translationLogic = gameObj.GetComponent<TranslationCardLogic>();
                if (translationLogic != null)
                {
                    translationLogic.uiController = translationUIController;
                    translationLogic.setCard(card);
                    translationLogic.onCardSelected = OnCardSelected;
                }
                break;
            case Card.cardType.MultipleChoice:
                gameObj = Instantiate(MultipleChoiceCard, cardArea);
                break;
            case Card.cardType.Matching:
                gameObj = Instantiate(MatchingCard, cardArea);
                break;
        }

        if (gameObj != null)
        {
            currentCards.Add(gameObj);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(cardArea);
    }

    private void ClearCards()
    {
        foreach (var cardObj in currentCards)
        {
            Destroy(cardObj);
        }
        currentCards.Clear();
    }

    private void OnCardSelected()
    {
        // Called when any card is selected
        ClearCards();
    }
}
