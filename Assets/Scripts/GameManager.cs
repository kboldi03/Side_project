using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Party")]
    public List<CharacterSaveData> party = new List<CharacterSaveData>();
    public List<string> inventory = new List<string>();

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

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}