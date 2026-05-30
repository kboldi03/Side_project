using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitCard : MonoBehaviour
{
    [Header("Front Face")]
    public Image characterSprite;
    public TMP_Text nameText;
    public TMP_Text classText;

    [Header("Back Face")]
    public TMP_Text statsText;

    [Header("Faces")]
    public GameObject frontFace;
    public GameObject backFace;

    private CharacterStats recruitStats;
    private bool isFlipped = false;

    public void Setup(CharacterStats stats)
    {
        recruitStats = stats;

        nameText.text = stats.characterName;
        classText.text = stats.classData.className;
        characterSprite.sprite = stats.classData.sprite;

        statsText.text =
            "HP: " + stats.maxHP + "\n" +
            "Attack: " + stats.attack + "\n" +
            "Magic: " + stats.magic + "\n" +
            "Armor: " + stats.armor + "\n" +
            "Resistance: " + stats.resistance + "\n" +
            "Speed: " + stats.speed + "\n" +
            "Crit: " + stats.crit + "\n" +
            "Armor Penetration: " + stats.armorPen + "\n" +
            "Magic Penetration: " + stats.magicPen;
    }

    public void OnInspectClicked()
    {
        isFlipped = !isFlipped;
        frontFace.SetActive(!isFlipped);
        backFace.SetActive(isFlipped);
    }

    public void OnHireClicked()
    {

        if (GameManager.instance.party.Count >= 4)
        {
            Debug.Log("Party is full");
            return;
        }
        GameManager.instance.party.Add(recruitStats.ToSaveData());
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
}