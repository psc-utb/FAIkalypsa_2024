using InteractionSystem.Interfaces;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour, IInteractable, IInformable<string>, IClosable
{
    [SerializeField]
    GameObject doorToInteract;

    Animator _animator;

    [SerializeField]
    bool opened = true;

    [SerializeField]
    UnityEvent<string> InteractionInfoRequested;

    [SerializeField]
    InputActionAsset inputActionAsset; 

    // Start is called before the first frame update
    void Start()
    {
        _animator = doorToInteract.GetComponent<Animator>();
    }

    public void Close()
    {
        _animator.SetTrigger("Close");
    }

    /*public void Open()
    {
        _animator.SetTrigger("Open");
    }*/

    public void Interact()
    {
        if (opened)
        {
            Close();
            opened = false;
        }
        /*else
        {
            Open();
            opened = true;
        }*/
    }

    public string Inform()
    {
        string textWithInformation = string.Empty;
        if (opened)
        {
            var bindingKey = inputActionAsset
                            .actionMaps
                            .FirstOrDefault(inputActionMap => inputActionMap.name == "Player")
                            .actions
                            .FirstOrDefault(action => action.name == "Interaction")
                            .GetBindingDisplayString();
            textWithInformation = $"Press {bindingKey} to close";
            InteractionInfoRequested?.Invoke(textWithInformation);
        }
        /*else
        {
            InteractionInfoRequested?.Invoke("Press E to open");
        }*/

        return textWithInformation;
    }
}
