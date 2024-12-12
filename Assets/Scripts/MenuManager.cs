using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string mainMenuSceneName;

    AudioListener audioListenerInScene;

    [SerializeField]
    InputActionAsset inputActionAsset;
    InputAction menuAction;

    // set things up here
    void Awake()
    {
        audioListenerInScene = FindObjectOfType<AudioListener>();   //najde audio listener ve scene (mel by existovat jen jeden)

        if(inputActionAsset != null)
            menuAction = inputActionAsset.FindAction("Menu");
    }

    // game loop
    void Update()
    {

        if (menuAction.IsPressed())
        {
            if (Time.timeScale > 0f)
            {
                audioListenerInScene.enabled = false;	//nesmi existovat 2 audio listenery ve scene
                SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Additive);
                Time.timeScale = 0f;
            }
            else
            {
                AsyncOperation asyncOpUnloadScene = SceneManager.UnloadSceneAsync(mainMenuSceneName);
                asyncOpUnloadScene.completed += UnloadScene_completed;	//jedna se o asynchronni metodu, takze musime pockat na dokonceni - v Unity je to mozne pomoci eventu
                Time.timeScale = 1f;
            }
        }
    }

    private void UnloadScene_completed(AsyncOperation obj)
    {
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;	//po unloadu sceny se musi audio listener znovu aktivovat
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;
    }
}
