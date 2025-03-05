using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InteractionActivator : Activator<IInteractable>
{
    [SerializeField]
    protected UnityEvent<IInteractable> objectActivated;

    protected new void Awake()
    {
        base.Awake();
        //objectActivated.AddListener(Interaction);
    }

    /*public void Interaction(IInteractable objectToInteract)
    {
        objectToInteract.Interact();
    }*/

    protected override void Activation(IInteractable obj)
    {
        obj.Interact();
        objectActivated?.Invoke(obj);
    }
}
