using InteractionSystem.Interfaces;

public class InteractionActivator : Activator<IInteractable>
{
    protected new void Awake()
    {
        base.Awake();
    }

    protected override void Activation(IInteractable obj)
    {
        obj.Interact();
    }
}
