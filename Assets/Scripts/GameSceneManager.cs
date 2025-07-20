using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadBattle()
    {
        SceneManager.LoadScene(SceneNames.COMBATSCENE);
    }

    public void BattleEnd()
    {
        SceneManager.LoadScene(SceneNames.TESTSCENE);
    }
}
