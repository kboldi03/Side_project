using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CombatUI : MonoBehaviour
{
    [Header("References")]
    public CombatManager combatManager;
    public TMP_Text turnIndicator;
    public Transform skillPanel;
    public GameObject skillButtonPrefab;

    private SkillData selectedSkill;
    private List<GameObject> skillList = new List<GameObject>();

    void Start()
    {
        StartCoroutine(UpdateUI());
    }

    IEnumerator UpdateUI()
    {
        CharacterStats current = combatManager.getCurrentCombatant();
        bool isPlayerTurn = combatManager.partyMembers.Contains(current);

        foreach (CharacterStats c in combatManager.partyMembers)
            if (c.turnArrow != null) c.turnArrow.SetActive(false);
        foreach (CharacterStats c in combatManager.enemies)
            if (c.turnArrow != null) c.turnArrow.SetActive(false);

        current.turnArrow?.SetActive(true);

        if (!isPlayerTurn)
        {
            turnIndicator.text = current.characterName + "'s turn";
            yield return new WaitForSeconds(1f);

            combatManager.ExecuteEnemyTurn(current);

            if (combatManager.isDefeat())
            {
                turnIndicator.text = "Defeat!";
                ClearSkillList();
                yield break;
            }

            combatManager.NextTurn();
            StartCoroutine(UpdateUI());
        }
        else
        {
            turnIndicator.text = current.characterName + "'s turn";
            SpawnSkillList(current);
        }
    }

    void SpawnSkillList(CharacterStats current)
    {
        
        ClearSkillList();

        foreach(SkillData skill in current.classData.skills)
        {
            GameObject btn = Instantiate(skillButtonPrefab, skillPanel);
            skillList.Add(btn);

            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            btnText.text = skill.skillName;
            
            Button button = btn.GetComponent<Button>();
            SkillData clickedSkill = skill;
            button.onClick.AddListener(() => OnSkillSelected(clickedSkill));

        }
    }

    void ClearSkillList()
    {
        foreach(GameObject btn in skillList)
        {
            Destroy(btn);
        }
        skillList.Clear();
    }

    void OnSkillSelected(SkillData skill)
    {

        if (skill.targetType == TargetType.AllEnemies)
        {
            ExecuteSkill(combatManager.enemies);
        }
        else if(skill.targetType == TargetType.AllAllies)
        {
            ExecuteSkill(combatManager.partyMembers);
        }
        else if(skill.targetType == TargetType.Self)
        {
            List<CharacterStats> self = new List<CharacterStats>();
            self.Add(combatManager.getCurrentCombatant());
            ExecuteSkill(self);
        }
        else
        {
            selectedSkill = skill;
            turnIndicator.text = "Select a target";
            
        }

    }

    void ExecuteSkill(List<CharacterStats> targets)
    {
        CharacterStats attacker = combatManager.getCurrentCombatant();

        foreach (CharacterStats target in targets)
        {
            if (selectedSkill.damageType != DamageType.None)
            {
                
                int damage = attacker.CalculateAttackDamage(selectedSkill.damageType == DamageType.Magical);
                target.TakeDamage(damage, selectedSkill.damageType == DamageType.Magical, 
                                  selectedSkill.damageType == DamageType.Magical ? attacker.magicPen : attacker.armorPen);
                combatManager.HandleDeath(target);
            }
        }

        if (combatManager.isVictory())
        {
            combatManager.DistributeXP();
            turnIndicator.text = "Victory!";
            ClearSkillList();
            return;
        }

        combatManager.NextTurn();
        selectedSkill = null;
        ClearSkillList();
        StartCoroutine(UpdateUI());
    }

    public void OnEntityClicked(CharacterStats entity)
    {
        if (selectedSkill == null) return;

        if(selectedSkill.targetType == TargetType.SingleEnemy && combatManager.enemies.Contains(entity))
        {
            List<CharacterStats> target = new List<CharacterStats> { entity };
            ExecuteSkill(target);
        }
        else if(selectedSkill.targetType == TargetType.SingleAlly && combatManager.partyMembers.Contains(entity))
        {
            List<CharacterStats> target = new List<CharacterStats> { entity };
            ExecuteSkill(target);
        }
    }
}
