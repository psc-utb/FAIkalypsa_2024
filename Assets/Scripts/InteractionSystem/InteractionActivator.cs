using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InteractionActivator : Activator<IInteractable>
{
    /*[SerializeField]
    protected UnityEvent<IInteractable> objectActivated;*/

    protected new void Awake()
    {
        base.Awake();
    }

    protected override void Activation(IInteractable obj)
    {
        obj.Interact();
        //objectActivated?.Invoke(obj);
    }

    public new void Activate()
    {
        bool activated = base.Activate();
    }
}
