using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public void LoadCombat()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void LoadCity()
    {
        SceneManager.LoadScene("CityScene");
    }
}
