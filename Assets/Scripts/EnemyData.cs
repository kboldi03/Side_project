using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;

    [Header("Stat Ranges")]
    public int minHP; public int maxHP;
    public int minAttack; public int maxAttack;
    public int minMagic; public int maxMagic;
    public int minArmor; public int maxArmor;
    public int minResistance; public int maxResistance;
    public int minSpeed; public int maxSpeed;
    public int minArmorPen; public int maxArmorPen;
    public int minMagicPen; public int maxMagicPen;

    [Header("Combat")]
    public bool usesMagic;
    public int xpReward;
}