using InteractionSystem.Interfaces;
using UnityEngine;

public class InformationActivator<T> : Activator<IInformable<T>>, IDeactivator
{
    [SerializeField]
    Component displayManagement;
    protected IInformationActivableElement<T> _displayManagement;

    protected new void Awake()
    {
        base.Awake();
        _displayManagement = displayManagement.GetComponent<IInformationActivableElement<T>>();
    }
    public override void Activate(IInformable<T> obj)
    {
        if (obj != null)
        {
            Activation(obj);
        }
        else
        {
            Deactivate();
        }
    }

    protected override void Activation(IInformable<T> obj)
    {
        _displayManagement.SetInformation(obj.Inform());
        _displayManagement.IsActivated = true;
    }

    public void Deactivate()
    {
        _displayManagement.SetInformation(default);
        _displayManagement.IsActivated = false;
    }
}
