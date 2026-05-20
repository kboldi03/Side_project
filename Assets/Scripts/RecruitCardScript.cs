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
            "HP:  " + stats.maxHP + "\n" +
            "ATK: " + stats.attack + "\n" +
            "MAG: " + stats.magic + "\n" +
            "ARM: " + stats.armor + "\n" +
            "RES: " + stats.resistance + "\n" +
            "SPD: " + stats.speed + "\n" +
            "CRT: " + stats.crit;
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
        gameObject.SetActive(false);
        Debug.Log("Hired: " + recruitStats.characterName);

    }
}