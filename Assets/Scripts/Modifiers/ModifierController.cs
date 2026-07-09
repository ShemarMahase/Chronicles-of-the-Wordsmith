using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModifierController : MonoBehaviour
{
    [SerializeField] Modifier mod;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] GameObject infoPanel;
    public void SetMod(Modifier modifier)
    {
        mod = modifier;
    }

    void Start()
    {
        infoPanel = UIManager.instance.infoText;
        infoText = infoPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (infoPanel != null)
        {
            infoPanel.SetActive(false); // Start with the info hidden
        }

    }
    //When Mouse enters Modifier Icon, display information text above the corresponding card
    public void OnMouseEnter()
    {
        if (infoPanel != null && infoText != null)
        {
            infoText.text = mod.GetInfo();
            infoPanel.SetActive(true);
            RectTransform thisRect = this.GetComponent<RectTransform>();
            RectTransform infoPanelRect = infoPanel.GetComponent<RectTransform>();
            infoPanelRect.anchoredPosition = thisRect.anchoredPosition + new Vector2(125f, 75f);
        }
    }
    public void OnMouseExit()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    //Position the tooltip near the mouse cursor, redundancy
    void OnMouseOver()
    {
        RectTransform thisRect = this.GetComponent<RectTransform>();
        RectTransform infoPanelRect = infoPanel.GetComponent<RectTransform>();
        infoPanelRect.anchoredPosition = thisRect.anchoredPosition + new Vector2(125f, 75f);
    }
}
