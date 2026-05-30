using UnityEngine;
using System.Collections.Generic;

public class CombatSetup : MonoBehaviour
{

    [Header("Spawn Points")]
    public Transform[] partySlots;
    public Transform[] enemySlots;

    [Header("Prefab")]
    public GameObject characterPrefab;

    [Header("Party Classes")]
    public ClassData[] partyClasses;

    [Header("Enemy Data")]
    public EnemyData[] enemyDatas;

    [Header("References")]
    public CombatManager combatManager;

    [Header("All Classes")]
    public ClassData[] allClasses;

    public NameDatabase nameDatabase;

    void Awake()
    {
        SpawnParty();
        SpawnEnemies();
    }

    void SpawnParty()
    {
        if (GameManager.instance.party.Count == 0)
        {
            // fallback for testing - use manual classes
            for (int i = 0; i < partyClasses.Length; i++)
            {
                if (partyClasses[i] == null) continue;
                if (i >= partySlots.Length) break;

                CharacterStats character = EntityFactory.CreateCharacter(partyClasses[i], characterPrefab, nameDatabase);
                character.transform.position = partySlots[i].position;
                combatManager.partyMembers.Add(character);
            }
        }
        else
        {
            for (int i = 0; i < GameManager.instance.party.Count; i++)
            {

                CharacterStats character = EntityFactory.CreateFromSaveData(GameManager.instance.party[i], characterPrefab, allClasses);
                character.transform.position = partySlots[i].position;
                combatManager.partyMembers.Add(character);
            }
        }
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyDatas.Length; i++)
        {
            if (enemyDatas[i] == null) continue;
            if (i >= enemySlots.Length) break;

            CharacterStats enemy = EntityFactory.CreateEnemy(enemyDatas[i], characterPrefab);
            enemy.transform.position = enemySlots[i].position;
            combatManager.enemies.Add(enemy);
        }
    }

}
