using UnityEngine;
using System.Collections.Generic;

public class TavernManager : MonoBehaviour
{
    [Header("References")]
    public GameObject recruitCardPrefab;
    public Transform recruitsPanel;
    public GameObject characterPrefab;

    public NameDatabase nameDatabase;

    [Header("Available Classes")]
    public ClassData[] availableClasses;

    void Start()
    {
        GenerateRecruits();
    }

    void GenerateRecruits()
    {
        for (int i = 0; i < 3; i++)
        { 
            ClassData randomClass = availableClasses[Random.Range(0, availableClasses.Length)];

            // create a character with randomized stats
            CharacterStats stats = EntityFactory.CreateCharacter(randomClass, characterPrefab, nameDatabase);
            stats.gameObject.SetActive(false); // hide until hired

            // spawn a recruit card
            GameObject cardObj = Instantiate(recruitCardPrefab, recruitsPanel);
            RecruitCard card = cardObj.GetComponent<RecruitCard>();
            card.Setup(stats);


        }
    }
}