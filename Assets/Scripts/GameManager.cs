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
    [SerializeField]
    string victorySceneName;


    [SerializeField]
    GameObject QuitButton;

    void Awake()
    {
        // determine if Quit button should be shown
        if(QuitButton != null)
            displayQuitWhenAppropriate();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(playAgainSceneName);
    }

    public void DeathScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(deathSceneName);
    }

    public void VictoryScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(victorySceneName);
    }


    // determine if the QUIT button should be present based on what platform the game is running on
    void displayQuitWhenAppropriate()
    {
        switch (Application.platform)
        {
            // platforms that should have quit button
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
                QuitButton.SetActive(true);
                break;

            // platforms that should not have quit button
            // note: included just for demonstration purposed since
            // default will cover all of these. 
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WebGLPlayer:
                QuitButton.SetActive(false);
                break;

            // all other platforms default to no quit button
            default:
                QuitButton.SetActive(false);
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
