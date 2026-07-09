using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Transform playerPosition;
    private Slider playerHealthBar;

    private void Awake()
    {
        playerHealthBar = GetComponent<Slider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(playerHealthBar);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    //Sets Sliders current and maximum value
    public void InitializeHealthBar(float curHealth, float maxHealth)
    {
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = curHealth;
    }

    public void UpdatePlayerHealth(float newHealth)
    {
        playerHealthBar.value = newHealth;
    }
    void MoveToPlayer()
    {
        this.transform.position = (playerPosition.position + new Vector3(0,1,0));
    }
}
