using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CombatManager : MonoBehaviour
{
    [Header("Combatants")]
    public List<CharacterStats> partyMembers;
    public List<CharacterStats> enemies;
    private List<CharacterStats> defeatedEnemies = new List<CharacterStats>();

    private List<CharacterStats> turnOrder = new List<CharacterStats>();
    private int currentTurnIndex = 0;

    void Start()
    {
        BuildTurnOrder();
    }

    void BuildTurnOrder()
    {
        turnOrder.Clear();
        foreach (CharacterStats c in partyMembers)
        {
            if (c != null) turnOrder.Add(c);
        }
        foreach (CharacterStats c in enemies)
        {
            if (c != null) turnOrder.Add(c);
        }



        turnOrder = turnOrder.OrderByDescending(c =>c.speed).ToList();
    }

    public CharacterStats getCurrentCombatant()
    {
        return turnOrder[currentTurnIndex];
    }

    public void NextTurn()
    {
        currentTurnIndex++;
        if(currentTurnIndex >= turnOrder.Count)
        {
            currentTurnIndex = 0;
        }
        
    }

    public void ExecuteEnemyTurn(CharacterStats enemy)
    {
        if (partyMembers.Count == 0) return;

        int randomIndex = Random.Range(0, partyMembers.Count);
        CharacterStats target = partyMembers[randomIndex];

        int damage = enemy.CalculateAttackDamage(enemy.usesMagic);
        target.TakeDamage(damage, enemy.usesMagic, enemy.usesMagic ? enemy.magicPen : enemy.armorPen);

        HandleDeath(target);
    }

    public void HandleDeath(CharacterStats character)
    {
        if(character.isDead())
        {
            turnOrder.Remove(character);

            if(enemies.Contains(character))
            {
                defeatedEnemies.Add(character);
                enemies.Remove(character);
            }
            else
            {
                partyMembers.Remove(character);
            }

            Destroy(character.gameObject);
        }
    }


    public void DistributeXP()
    {
        int totalXP = 0;
        foreach(CharacterStats enemy in defeatedEnemies)
        {
            totalXP += enemy.xpReward;
        }

        int xpShare = totalXP / partyMembers.Count;
        foreach(CharacterStats member in partyMembers)
        {
            member.GainXP(xpShare);
        }
    }

    public bool isVictory()
    {
        return enemies.Count == 0;
    }
    public bool isDefeat()
    {
        return partyMembers.Count == 0;
    }




    
}
