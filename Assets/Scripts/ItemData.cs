using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatModifier
{
    public StatType stat;
    public int value;
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public List<StatModifier> modifiers;
}