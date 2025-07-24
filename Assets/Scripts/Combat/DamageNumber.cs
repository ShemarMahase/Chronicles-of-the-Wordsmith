using UnityEngine;
using TMPro; // or UnityEngine.UI if you use Text

public class DamageNumber : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeDuration = 1f;
    private TextMeshProUGUI textMesh;
    private Color originalColor;
    private float timer;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalColor = textMesh.color;
        timer = 0;
    }

    public void SetDamage(float dmg)
    {
        textMesh.text = dmg.ToString("0");
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;

        // Calculate fade-out effect: alpha goes from 1 → 0 over fadeDuration
        float alpha = Mathf.Lerp(originalColor.a, 0, timer / fadeDuration);

        // Apply new color with updated alpha (fade effect)
        textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - timer / fadeDuration);

        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
