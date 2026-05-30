using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Game/Ability")]
public class SkillData : ScriptableObject
{
    [Header("Identity")]
    public string skillName;
    public string description;
    public Sprite icon;

    [Header("Targeting")]
    public TargetType targetType;
    public DamageType damageType;

    [Header("Effects")]
    public float damageMultiplier = 1f;
    public float healAmount = 1f;
    public int buffAmount;
    public BuffType statToBuff;
    public int buffDuration;
}
