using TMPro;
using UnityEngine;

public class TranslationInputUIController : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI cardText;
    public TextMeshProUGUI timerText;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private Card currentCard;
    private float timeLeft = 10f;
    private bool timerRunning = false;
    private bool isComplete = false;

    void Awake()
    {
        gameObject.SetActive(false);
        resultPanel.SetActive(false);
    }


    void Update()
    {
        //stop loop if minigame ends
        if (!timerRunning || isComplete) return;

        //timer for minigame
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timeLeft).ToString();

        if (timeLeft <= 0f)
        {
            timerRunning = false;
            ShowResult(false);
        }
    }


    public void StartMiniGame(Card card)
    {
        resultPanel.SetActive(false);

        //initialize Minigame visuals and timer
        currentCard = card;
        cardText.text = card.text;
        inputField.text = "";
        resultPanel.SetActive(false);
        inputField.ActivateInputField();

        timeLeft = 10f;
        timerRunning = true;
        isComplete = false;

        inputField.onValueChanged.RemoveAllListeners();
        inputField.onValueChanged.AddListener(OnInputChanged);

        gameObject.SetActive(true);  // Show the UI panel
    }

    private void OnInputChanged(string userInput)
    {
        if (isComplete || !timerRunning) return;

        //Ignore empty or too short inputs to prevent false positives
        if (string.IsNullOrWhiteSpace(userInput) || userInput.Length < 2)
        return;

        //stop timer early if correct
        string correctAnswer = currentCard.GetCorrectTranslation();
        if (string.Equals(userInput.Trim(), correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            isComplete = true;
            timerRunning = false;
            ShowResult(true);
        }
    }

    private void ShowResult(bool isCorrect)
    {
        resultPanel.SetActive(true);
        resultText.text = isCorrect ? "Correct!" : $"Wrong! \nAnswer: \n{currentCard.GetCorrectTranslation()}";
    }
}
