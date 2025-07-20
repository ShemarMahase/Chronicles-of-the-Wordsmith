using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardLogic : MonoBehaviour
{
    private Card card;
    public TextMeshProUGUI cardText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //attaches card to script and changes card data
    public void setCard(Card cardData)
    {
        card = cardData;
        cardText.text = card.text;
    }
}
