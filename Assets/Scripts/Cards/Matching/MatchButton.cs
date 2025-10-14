using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchButton : MonoBehaviour
{
    public int buttonID;
    public TextMeshProUGUI text;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
            FindFirstObjectByType<LineDrawer>().ButtonSelected(GetComponent<Button>(), buttonID));
    }
}
