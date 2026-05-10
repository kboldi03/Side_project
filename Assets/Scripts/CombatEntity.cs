using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    private CombatUI combatUI;

    private void Start()
    {
        combatUI = FindAnyObjectByType<CombatUI>();
    }

    private void OnMouseDown()
    {
        combatUI.OnEntityClicked(GetComponent<CharacterStats>());
    }
}
