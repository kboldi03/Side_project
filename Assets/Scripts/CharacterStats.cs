using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [Header("Identity")]
    public string characterName;
    public ClassData classData;

    [Header("UI")]
    public GameObject turnArrow;

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

    [Header("Permanent Bonuses")]
    public int permAttack;
    public int permMagic;
    public int permArmor;
    public int permResistance;
    public int permSpeed;
    public int permCrit;

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

        FloatingTextSpawner.instance.Spawn(finalDamage.ToString(), transform.position, Color.red);

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

    public CharacterSaveData ToSaveData()
    {
        CharacterSaveData data = new CharacterSaveData();
        data.characterName = characterName;
        data.className = classData != null ? classData.className : "";
        data.maxHP = maxHP;
        data.currentHP = currentHP;
        data.attack = attack;
        data.magic = magic;
        data.armor = armor;
        data.resistance = resistance;
        data.speed = speed;
        data.armorPen = armorPen;
        data.magicPen = magicPen;
        data.crit = crit;
        data.level = level;
        data.currentXP = currentXP;
        data.xpToNextLevel = xpToNextLevel;
        data.permAttack = permAttack;
        data.permMagic = permMagic;
        data.permArmor = permArmor;
        data.permResistance = permResistance;
        data.permSpeed = permSpeed;
        data.permCrit = permCrit;
        data.usesMagic = usesMagic;
        data.xpReward = xpReward;
        return data;
    }
}
