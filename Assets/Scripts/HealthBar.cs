using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private CharacterStats stats;

    void Start()
    {
        slider = GetComponent<Slider>();
        stats = GetComponentInParent<CharacterStats>();
    }

    void Update()
    {
        if (stats == null) return;
        slider.value = (float)stats.currentHP / (float)stats.maxHP;
    }

}
