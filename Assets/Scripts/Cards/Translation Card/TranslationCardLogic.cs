using System;
using UnityEngine;

public class TranslationCardLogic : CardLogic
{
    public TranslationInputUIController uiController;
    public Action onCardSelected;

    //Start the minigame when a card is selected
    public void OnCardClicked()
    {
        Debug.Log("enabling");
        UIManager.instance.EnableTranslationGame();
        Debug.Log("broadcasting event");
        uiController = UIManager.instance.GetTranslationGame();
        onCardSelected?.Invoke();
        if (uiController != null)
        {
            uiController.StartMiniGame(card);
        }
        else
        {
            Debug.LogWarning("TranslationInputUIController not assigned!");
        }
    }

    public override void setCard(Card cardData)
    {
        base.setCard(cardData);
        // No extra setup here unless needed
    }
}
