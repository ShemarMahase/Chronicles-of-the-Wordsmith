using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardLogic : MonoBehaviour
{
    protected Card card;
    public TextMeshProUGUI cardText;
    public Modifier mod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //attaches card to script and changes card data
    public virtual void SetCard(Card cardData)
    {
        card = cardData;
        cardText.text = card.text;
    }

    public void playAudio()
    {
        AudioManager.instance.PlayAudio(card.audioClip);
    }
}
