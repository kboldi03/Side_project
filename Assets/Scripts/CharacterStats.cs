using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Identity")]
    public string characterName;
    public ClassData classData;

    [Header("Enemy Data")]
    public int xpReward;
    public bool usesMagic;

    [Header("Base Stats")]
    public int maxHP;
    public int currentHP;
    public int attack;
    public int magic;
    public int armor;
    public int resistance;
    public int speed;
    public int armorPen;
    public int magicPen;
    public int crit;

    [Header("Level")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;

    public void GainXP(int amount)
    {
        currentXP += amount;
        if(currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        currentXP -= xpToNextLevel;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        if(classData != null)
        {
            maxHP += classData.hpGrowth;
            currentHP += classData.hpGrowth;
            attack += classData.attackGrowth;
            magic += classData.magicGrowth;
            armor += classData.armorGrowth;
            resistance += classData.resistanceGrowth;
            speed += classData.speedGrowth;
            armorPen += classData.armorPenGrowth;
            magicPen += classData.magicPenGrowth;
            crit += classData.critGrowth;
        }

    }
    public bool isDead()
    { return currentHP <= 0; }

    public void TakeDamage(int damage, bool isMagic, int attackPen)
    {
        int effectiveDefense;

        if (isMagic)
        {
            effectiveDefense = Mathf.Max(0, resistance - attackPen);
        }
        else
        {
            effectiveDefense = Mathf.Max(0, armor - attackPen);
        }

        int finalDamage = Mathf.Max(1, damage - effectiveDefense);
        if (currentHP - finalDamage <= 0)
        {
            currentHP = 0;
        }
        else
        {
            currentHP -= finalDamage;
        }

    }

    public int CalculateAttackDamage(bool isMagic)
    {
        int baseDamage;
        if (isMagic)
        {
            baseDamage = magic;
        }
        else
        {
            baseDamage = attack;
        }
        bool isCrit = Random.Range(0, 100) < crit;
        if (isCrit)
        {
            return Mathf.RoundToInt(baseDamage * 1.7f);
        }
        return baseDamage;
    }
}
