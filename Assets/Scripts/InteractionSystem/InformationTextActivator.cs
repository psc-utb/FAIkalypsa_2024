using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InformationTextActivator : Activator<IInformable<string>>
{
    [SerializeField]
    protected UnityEvent<IInformable<string>> objectActivated;

    protected new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Activate() == false)
            objectActivated?.Invoke(null);
    }

    protected override void Activation(IInformable<string> obj)
    {
        obj.Inform();
        objectActivated?.Invoke(obj);
    }
}
