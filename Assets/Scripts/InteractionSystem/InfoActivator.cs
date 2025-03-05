using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InfoActivator : Activator<IInformable<string>>
{
    [SerializeField]
    protected UnityEvent<IInformable<string>> objectWithInfoDetected;

    protected new void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Activate() == false)
            objectWithInfoDetected?.Invoke(null);
    }

    protected override void Activation(IInformable<string> obj)
    {
        objectWithInfoDetected?.Invoke(obj);
    }
}
