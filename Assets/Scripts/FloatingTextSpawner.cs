using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner instance;
    public GameObject floatingTextPrefab;
    public Canvas canvas;

    void Awake()
    {
        instance = this;
    }

    public void Spawn(string message, Vector3 worldPosition, Color color)
    {
        GameObject go = Instantiate(floatingTextPrefab, canvas.transform);
        go.transform.position = worldPosition + new Vector3(0, 0.5f, 0);
        FloatingText ft = go.GetComponent<FloatingText>();
        ft.SetText(message, color);
    }
}