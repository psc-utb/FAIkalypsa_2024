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

    public void Interact()
    {
        if (opened)
        {
            Close();
        }
    }

    public void ShowInteraction()
    {
        InteractionInfoRequested?.Invoke("Press E to close");
    }
}
