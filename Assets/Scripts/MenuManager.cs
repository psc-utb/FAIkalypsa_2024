using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string menuSceneName;

    AudioListener audioListenerInScene;
    [SerializeField]
    GameObject EventSystem;

    [SerializeField]
    InputActionAsset inputActionAsset;
    InputAction menuAction;

    // set things up here
    void Awake()
    {
        audioListenerInScene = FindObjectOfType<AudioListener>();   //najde audio listener ve scene (mel by existovat jen jeden)

        if(inputActionAsset != null)
        {
            menuAction = inputActionAsset
                            .actionMaps
                            .FirstOrDefault(inputActionMap => inputActionMap.name == "UI")
                            .actions
                            .FirstOrDefault(action => action.name == "Menu");
            //menuAction = inputActionAsset..FindAction("Menu");
        }

    }

    // game loop
    void Update()
    {
        if (menuAction.IsPressed())
        {
            ShowHideMenu();
        }
    }

    public void ShowHideMenu()
    {
        if (Time.timeScale > 0f)
        {
            audioListenerInScene.enabled = false;   //nesmi existovat 2 audio listenery ve scene
            EventSystem.SetActive(false);   //nesmi existovat 2 event systemy ve scene
            SceneManager.LoadScene(menuSceneName, LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }
        else
        {
            AsyncOperation asyncOpUnloadScene = SceneManager.UnloadSceneAsync(menuSceneName);
            asyncOpUnloadScene.completed += UnloadScene_completed;  //jedna se o asynchronni metodu, takze musime pockat na dokonceni - v Unity je to mozne pomoci eventu
        }
    }

    private void UnloadScene_completed(AsyncOperation obj)
    {
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;    //po unloadu sceny se musi audio listener znovu aktivovat
        if (EventSystem != null)
            EventSystem.SetActive(true);   //po unloadu sceny se musi event system znovu aktivovat
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;
    }
}
