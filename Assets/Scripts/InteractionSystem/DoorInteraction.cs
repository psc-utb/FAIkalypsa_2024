using InteractionSystem.Interfaces;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject doorToInteract;

    Animator _animator;
    //IOpenable doorState;


    void Start()
    {
        _animator = doorToInteract.GetComponent<Animator>();
        //doorState = doorToInteract.GetComponent<IOpenable>();
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
        if (/*doorState.Opened*/ _animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            Close();
            //doorState.Opened = false;
        }
        /*else
        {
            Open();
            doorState.Opened = true;
        }*/
    }
}
