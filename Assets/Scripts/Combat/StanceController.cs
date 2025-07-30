using UnityEngine;
using UnityEngine.UI;

public class StanceController : MonoBehaviour
{
    Stance stance;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void SetStance(Stance stance)
    {
        this.stance = stance;
    }    

    public void OnClick()
    {
        TurnManager.instance.SetStance(stance);
    }
}
