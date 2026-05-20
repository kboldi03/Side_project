using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Party")]
    public List<CharacterSaveData> party = new List<CharacterSaveData>();

    [Header("Corruption")]
    public int corruptionTicks = 0;
    public int[] floorCorruptionLimits;

    [Header("Dungeon")]
    public int currentFloor = 1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}