using UnityEngine;

[CreateAssetMenu(fileName = "NameDatabase", menuName = "Game/NameDatabase")]
public class NameDatabase : ScriptableObject
{
    public string[] maleNames;
    public string[] femaleNames;

    public string GetRandom(Gender gender)
    {
        string[] names = gender == Gender.Male ? maleNames : femaleNames;
        return names[Random.Range(0, names.Length)];
    }
}