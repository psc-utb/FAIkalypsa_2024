using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class DoorController : MonoBehaviour, IClosable
{
    Animator _animator;

    [SerializeField]
    bool opened = true;

    public UnityEvent<string> InteractionInfoRequested;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
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
