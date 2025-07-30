using TMPro;
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

    public void OnMouseEnter()
    {
        if (infoPanel != null && infoText != null)
        {
            infoText.text = mod.GetInfo();
            infoPanel.SetActive(true);
        }
    }
    public void OnMouseExit()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
    }

    // Optional: Position the tooltip near the mouse cursor
    void OnMouseOver()
    {
        if (infoPanel != null)
        {
            // Adjust offset as needed
            infoPanel.transform.position = Input.mousePosition + new Vector3(20, -20, 0);
        }
    }
}
