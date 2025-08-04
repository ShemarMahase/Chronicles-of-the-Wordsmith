using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class HandVisualizer : MonoBehaviour
{
    public TranslationInputUIController translationUIController;
    public MultipleChoiceController multipleChoiceController;
    public GameObject TranslationCard;
    public GameObject MatchingCard;
    public GameObject MultipleChoiceCard;
    public GameObject StanceCard;
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

    void Start()
    {
        UIManager.instance.onCardSelected = OnCardSelected;
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
                    translationLogic.mod = card.mod;
                    translationLogic.uiController = translationUIController;
                    translationLogic.SetCard(card);
                    translationLogic.onCardSelected = OnCardSelected;
                }
                break;
            case Card.cardType.MultipleChoice:
                gameObj = Instantiate(MultipleChoiceCard, cardArea);
                MultipleChoiceLogic cardLogic = gameObj.GetComponent<MultipleChoiceLogic>();
                cardLogic.SetCard(card);
                cardLogic.SetController(multipleChoiceController);
                break;
            case Card.cardType.Matching:
                gameObj = Instantiate(MatchingCard, cardArea);
                break;
            case Card.cardType.Stance:
                gameObj = Instantiate(StanceCard, cardArea);
                StanceController sc = gameObj.gameObject.GetComponent<StanceController>();
                sc.SetStance(card as Stance);
                Debug.Log("Stance set to " + (card as Stance).text);
                gameObj.gameObject.GetComponent<Image>().sprite = (card as Stance).GetImage();
                break;
        }

        if (gameObj != null)
        {
            currentCards.Add(gameObj);
            if (card.mod)
            {
                Image mod = gameObj.transform.Find("Buff Icon").GetComponent<Image>();
                if (card.mod.GetSprite()) mod.sprite = card.mod.GetSprite();
                ModifierController modifierController = gameObj.GetComponent<ModifierController>();
                modifierController.SetMod(card.mod);
            }
            else
            {
                GameObject buffIcon = gameObj.transform.Find("Buff Icon").gameObject;
                buffIcon.SetActive(false);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(cardArea);
    }

    private void ClearCards()
    {
        Debug.Log("deleting cards");
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
