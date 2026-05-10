using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeDuration = 1f;
    private TMP_Text text;
    private Color startColor;
    private float timer = 0f;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        startColor = text.color;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
        text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string message, Color color)
    {
        text = GetComponent<TMP_Text>();
        text.text = message;
        text.color = color;
        startColor = color;
    }
}
