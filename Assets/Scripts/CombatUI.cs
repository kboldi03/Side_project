using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Header("References")]
    public CombatManager combatManager;
    public TMP_Text combatLog;
    public Button attackButton;

    void Start()
    {
        attackButton.onClick.AddListener(OnAttackPressed);
        UpdateUI();
    }

    void UpdateUI()
    {
        CharacterStats current = combatManager.getCurrentCombatant();

        bool isPlayerTurn = combatManager.partyMembers.Contains(current);
        attackButton.gameObject.SetActive(isPlayerTurn);

        if (!isPlayerTurn)
        {
            combatManager.ExecuteEnemyTurn(current);
            combatLog.text = current.characterName + " attacked!";

            if (combatManager.isDefeat())
            {
                combatLog.text = "Defeat!";
                attackButton.gameObject.SetActive(false);
                return;
            }

            combatManager.NextTurn();
            UpdateUI();
        }
        else
        {
            combatLog.text = "Current turn: " + current.characterName;
        }
    }

    void OnAttackPressed()
    {
        CharacterStats attacker = combatManager.getCurrentCombatant();
        CharacterStats target = combatManager.enemies[0];

        int damage = attacker.CalculateAttackDamage(attacker.usesMagic);
        target.TakeDamage(damage, attacker.usesMagic, attacker.usesMagic ? attacker.magicPen : attacker.armorPen);

        Debug.Log(attacker.characterName + " hits " + target.characterName + " for " + damage + " damage. Target HP: " + target.currentHP);

        combatManager.HandleDeath(target);

        if (combatManager.isVictory())
        {
            combatLog.text = "Victory!";
            attackButton.gameObject.SetActive(false);
            return;
        }

        combatManager.NextTurn();
        UpdateUI();
    }

}
