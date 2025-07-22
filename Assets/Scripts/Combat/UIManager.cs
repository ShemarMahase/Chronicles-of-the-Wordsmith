using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //UI and buttons
    public Button Act;
    public Button Shuffle;
    public Button Item;
    public RectTransform TranslationGame;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisableActionUI()
    {
        Act.gameObject.SetActive(false);
        Shuffle.gameObject.SetActive(false);
        Item.gameObject.SetActive(false);
    }

    public void EnableActionUI()
    {
        Act.gameObject.SetActive(true);
        Shuffle.gameObject.SetActive(true);
        Item.gameObject.SetActive(true);
    }

    public void DisableTranslationGame()
    {
        TranslationGame.gameObject.SetActive(false);
    }
    public void EnableTranslationGame()
    {
        TranslationGame.gameObject.SetActive(true);
        Debug.Log("should be active now");
    }

    public TranslationInputUIController GetTranslationGame()
    {
        return TranslationGame.gameObject.GetComponent<TranslationInputUIController>();
    }
}
