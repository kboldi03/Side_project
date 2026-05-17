using UnityEngine;

public class EntityFactory : MonoBehaviour 
{
    
    public static CharacterStats CreateCharacter(ClassData classData, GameObject prefab, int floorBonus = 0)
    {
        GameObject go = Instantiate(prefab);
        CharacterStats stats = go.GetComponent<CharacterStats>();
        go.GetComponent<SpriteRenderer>().sprite = classData.sprite;

        stats.characterName = classData.className;
        stats.classData = classData;
        stats.maxHP = Random.Range(classData.minHP, classData.maxHP) + floorBonus;
        stats.currentHP = stats.maxHP;
        stats.attack = Random.Range(classData.minAttack, classData.maxAttack) + floorBonus;
        stats.magic = Random.Range(classData.minMagic, classData.maxMagic) + floorBonus;
        stats.armor = Random.Range(classData.minArmor, classData.maxArmor) + floorBonus;
        stats.resistance = Random.Range(classData.minResistance, classData.maxResistance) + floorBonus;
        stats.speed = Random.Range(classData.minSpeed, classData.maxSpeed) + floorBonus;
        stats.armorPen = Random.Range(classData.minArmorPen, classData.maxArmorPen);
        stats.magicPen = Random.Range(classData.minMagicPen, classData.maxMagicPen);
        stats.crit = Random.Range(classData.minCrit, classData.maxCrit);

        return stats;
    }

    public static CharacterStats CreateEnemy(EnemyData enemyData, GameObject prefab)
    {
        GameObject go = Instantiate(prefab);
        CharacterStats stats = go.GetComponent<CharacterStats>();
        go.GetComponent<SpriteRenderer>().sprite = enemyData.sprite;

        stats.characterName = enemyData.enemyName;
        stats.usesMagic = enemyData.usesMagic;
        stats.xpReward = enemyData.xpReward;
        stats.maxHP = Random.Range(enemyData.minHP, enemyData.maxHP);
        stats.currentHP = stats.maxHP;
        stats.attack = Random.Range(enemyData.minAttack, enemyData.maxAttack);
        stats.magic = Random.Range(enemyData.minMagic, enemyData.maxMagic);
        stats.armor = Random.Range(enemyData.minArmor, enemyData.maxArmor);
        stats.resistance = Random.Range(enemyData.minResistance, enemyData.maxResistance);
        stats.speed = Random.Range(enemyData.minSpeed, enemyData.maxSpeed);
        stats.armorPen = Random.Range(enemyData.minArmorPen, enemyData.maxArmorPen);
        stats.magicPen = Random.Range(enemyData.minMagicPen, enemyData.maxMagicPen);

        return stats;
    }


}
