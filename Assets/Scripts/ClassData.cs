using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewClass", menuName = "Game/Class Data")]
public class ClassData : ScriptableObject
{

    [Header("Identity")]
    public string className;
    public int levelCap;
    public Gender gender;

    [Header("Visuals")]
    public Sprite sprite;

    [Header("Abilities")]
    public List<SkillData> skills = new List<SkillData>();

    [Header("Base Stat Ranges")]
    public int minHP; public int maxHP;
    public int minAttack; public int maxAttack;
    public int minMagic; public int maxMagic;
    public int minArmor; public int maxArmor;
    public int minResistance; public int maxResistance;
    public int minSpeed; public int maxSpeed;
    public int minArmorPen; public int maxArmorPen;
    public int minMagicPen; public int maxMagicPen;
    public int minCrit; public int maxCrit;

    [Header("Stat Growth Per Level")]
    public int hpGrowth;
    public int attackGrowth;
    public int magicGrowth;
    public int armorGrowth;
    public int resistanceGrowth;
    public int speedGrowth;
    public int armorPenGrowth;
    public int magicPenGrowth;
    public int critGrowth;

    [Header("Evolution")]
    public ClassData evolutionOptionA;
    public ClassData evolutionOptionB;
    public ClassData evolutionOptionC;

}
