using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController : MonoBehaviour, IClosable
{
    [SerializeField]
    GameObject doorToInteract;

    Animator _animator;

    [SerializeField]
    bool opened = true;

    public UnityEvent<string> InteractionInfoRequested;

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

    public void ShowInteraction()
    {
        if (opened)
        {
            InteractionInfoRequested?.Invoke("Press E to close");
        }
        /*else
        {
            InteractionInfoRequested?.Invoke("Press E to open");
        }*/
    }
}
