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

    private Dictionary<CharacterStats, GameObject> turnArrows = new Dictionary<CharacterStats, GameObject>();

    void Start()
    {
        BuildArrowDictionary();
        StartCoroutine(UpdateUI());
    }

    IEnumerator UpdateUI()
    {
        CharacterStats current = combatManager.getCurrentCombatant();
        bool isPlayerTurn = combatManager.partyMembers.Contains(current);

        foreach(var arrow in turnArrows.Values)
        {
            arrow.SetActive(false);
        }

        if(turnArrows.ContainsKey(current))
        {
            turnArrows[current].SetActive(true);
        }    

        if (!isPlayerTurn)
        {
            turnIndicator.text = current.characterName + "'s turn";
            yield return new WaitForSeconds(1f);

            combatManager.ExecuteEnemyTurn(current);

            foreach (var key in new List<CharacterStats>(turnArrows.Keys))
            {
                if(!combatManager.partyMembers.Contains(key) && !combatManager.enemies.Contains(key))
                {
                    turnArrows.Remove(key);
                    
                }
            }


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

    void BuildArrowDictionary()
    {
        foreach (CharacterStats c in combatManager.partyMembers)
            turnArrows[c] = c.transform.Find("Arrow").gameObject;
        foreach (CharacterStats c in combatManager.enemies)
            turnArrows[c] = c.transform.Find("Arrow").gameObject;
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
        selectedSkill = skill;
        turnIndicator.text = "Select a target";
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
                turnArrows.Remove(target);
            }
        }

        if (combatManager.isVictory())
        {
            combatManager.DistributeXP();
            combatManager.SavePartyToGameManager();
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

        List<CharacterStats> targets = new List<CharacterStats>();

        if(selectedSkill.targetType == TargetType.SingleEnemy && combatManager.enemies.Contains(entity))
        {
            targets.Add(entity);
        }
        else if(selectedSkill.targetType == TargetType.SingleAlly && combatManager.partyMembers.Contains(entity))
        {
            targets.Add(entity);
        }
        else if(selectedSkill.targetType == TargetType.AllEnemies)
        {
            targets.AddRange(combatManager.enemies);
        }
        else if(selectedSkill.targetType == TargetType.AllAllies)
        {
            targets.AddRange(combatManager.partyMembers);
        }
        else if(selectedSkill.targetType == TargetType.Self)
        {
            targets.Add(combatManager.getCurrentCombatant());
        }

        if (targets.Count > 0)
        {
            ExecuteSkill(targets);
        }
    }
}
