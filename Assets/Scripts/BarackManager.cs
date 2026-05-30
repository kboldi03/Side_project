using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BaracksManager : MonoBehaviour
{
    [Header("Party Panel")]
    public Transform partyPanel;
    public GameObject partyIconPrefab;

    [Header("Display Panel")]
    public GameObject displayPanel;
    public Image characterSprite;
    public TMP_Text nameText;
    public TMP_Text classText;
    public TMP_Text statsText;
    public Transform skillsPanel;
    public GameObject skillButtonPrefab;

    public Image itemIcon;
    public TMP_Text itemNameText;
    public ItemData[] allItems;

    [Header("Classes")]
    public ClassData[] allClasses;

    private List<GameObject> skillButtons = new List<GameObject>();

    void Start()
    {
        
        displayPanel.SetActive(false);
        SpawnPartyIcons();
    }

    void SpawnPartyIcons()
    {
        foreach (CharacterSaveData member in GameManager.instance.party)
        {
            GameObject icon = Instantiate(partyIconPrefab, partyPanel);
            CharacterSaveData captured = member;

            ClassData classData = FindClassData(member.className);
            if (classData != null)
            {
                Image iconImage = icon.GetComponentInChildren<Image>();
                iconImage.sprite = classData.sprite;
            }

            Button button = icon.GetComponent<Button>();
            button.onClick.AddListener(() => ShowMember(captured));
        }
    }

    void ShowMember(CharacterSaveData data)
    {
        displayPanel.SetActive(true);

        ClassData classData = FindClassData(data.className);

        if (classData != null)
        {
            characterSprite.sprite = classData.sprite;
        }

        nameText.text = data.characterName;
        classText.text = data.className;
        statsText.text =
            "Level: " + data.level + "\n" +
            "Experience points: " + data.currentXP + " / " + data.xpToNextLevel + "\n" +
            "Maximum health points: " + data.maxHP + "\n" +
            "Attack power: " + data.attack + "\n" +
            "Magic power: " + data.magic + "\n" +
            "Armor: " + data.armor + "\n" +
            "Resistance: " + data.resistance + "\n" +
            "Speed: " + data.speed + "\n" +
            "Critical hit chance: " + data.crit + "%" + "\n" +
            "Armor Penetration: " + data.armorPen + "\n" +
            "Magic Penetration: " + data.magicPen + "\n";
        /*
            "\n" + "Permanent Bonuses:" + "\n" +
            "Attack power: " + data.permAttack + "\n" +
            "Magic power: " + data.permMagic + "\n" +
            "Armor: " + data.permArmor + "\n" +
            "Resistance: " + data.permResistance + "\n" +
            "Speed: " + data.permSpeed + "\n" +
            "Critical hit chance: " + data.permCrit;
        */
        ClearSkillButtons();

        if (classData != null)
        {
            foreach (SkillData skill in classData.skills)
            {
                GameObject btn = Instantiate(skillButtonPrefab, skillsPanel);
                skillButtons.Add(btn);

                TMP_Text[] texts = btn.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text text in texts)
                {
                    if (text.gameObject.name == "Skill Name")
                        text.text = skill.skillName;
                    else if (text.gameObject.name == "Description")
                        text.text = skill.description;
                }

                // set icon
                Image[] images = btn.GetComponentsInChildren<Image>();
                foreach (Image img in images)
                {
                    if (img.gameObject.name == "Icon")
                    {
                        img.sprite = skill.icon;
                        break;
                    }
                }
            }
        }

        ItemData equippedItem = FindItem(data.equippedItemName);
        if (equippedItem != null)
        {
            itemIcon.sprite = equippedItem.icon;
            itemNameText.text = equippedItem.itemName;
        }
        else
        {
            itemIcon.sprite = null;
            itemNameText.text = "No item equipped";
        }
    }
    ItemData FindItem(string itemName)
    {
        foreach (ItemData item in allItems)
            if (item.itemName == itemName)
                return item;
        return null;
    }

    void ClearSkillButtons()
    {
        foreach (GameObject btn in skillButtons)
            Destroy(btn);
        skillButtons.Clear();
    }

    ClassData FindClassData(string className)
    {
        foreach (ClassData classData in allClasses)
            if (classData.className == className)
                return classData;
        return null;
    }
}