using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private string menuSceneName;

    AudioListener audioListenerInScene;
    [SerializeField]
    GameObject eventSystem;

    [SerializeField]
    GameObject player;
    GunInventoryCC GunInventory;

    //varianta s vygenerovanym C# souborem inputu
    InputActions inputActions;
    //InputAction menuAction;

    // set things up here
    void Awake()
    {
        audioListenerInScene = FindObjectOfType<AudioListener>();   //najde audio listener ve scene (mel by existovat jen jeden)

        //samostatna vlastni instance se vstupními akcemi
        inputActions = new InputActions();
        //menuAction = InputSystem.actions.FindAction("Menu");

        inputActions.UI.Menu.performed += callbackContext => ShowHideMenu();
        //menuAction.performed += callbackContext => ShowHideMenu();
    }

    //musi dojit k zapnuti mapovani na UI, jinak k eventu nikdy nedojde!
    private void OnEnable() => inputActions.UI.Enable();
    private void OnDisable() => inputActions.UI.Disable();

    private void Start()
    {
        GunInventory = FindObjectOfType<GunInventoryCC>();
    }

    // game loop
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideMenu();
        }*/
    }

    private void ShowHideMenu()
    {
        if (Time.realtimeSinceStartup > 1f)
        {
            if (Time.timeScale > 0f)
            {
                audioListenerInScene.enabled = false;   //nesmi existovat 2 audio listenery ve scene
                eventSystem.SetActive(false);   //nesmi existovat 2 event systemy ve scene

                Cursor.lockState = CursorLockMode.None;
                if (GunInventory.currentGun != null)
                    GunInventory.currentGun.SetActive(false);
                player.SetActive(false);

                SceneManager.LoadScene(menuSceneName, LoadSceneMode.Additive);
                Time.timeScale = 0f;
            }
            else
            {
                AsyncOperation asyncOpUnloadScene = SceneManager.UnloadSceneAsync(menuSceneName);
                asyncOpUnloadScene.completed += UnloadScene_completed;  //jedna se o asynchronni metodu, takze musime pockat na dokonceni - v Unity je to mozne pomoci eventu
            }
        }
    }

    private void UnloadScene_completed(AsyncOperation obj)
    {
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;    //po unloadu sceny se musi audio listener znovu aktivovat
        if (eventSystem != null)
            eventSystem.SetActive(true);   //po unloadu sceny se musi event system znovu aktivovat
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        player.SetActive(true);
        if (GunInventory.currentGun != null)
            GunInventory.currentGun.SetActive(true);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        if (audioListenerInScene != null)
            audioListenerInScene.enabled = true;
    }
}
