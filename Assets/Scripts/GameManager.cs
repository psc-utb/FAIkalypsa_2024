using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    string playAgainSceneName;
    [SerializeField]
    string deathSceneName;

    public void PlayAgain()
    {
        SceneManager.LoadScene(playAgainSceneName);
    }

    public void DeathScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(deathSceneName);
    }
}
