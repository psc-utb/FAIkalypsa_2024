using InteractionSystem.Interfaces;
using UnityEngine;

public class InformationActivator<T> : Activator<IInformable<T>>, IDeactivator
{
    [SerializeField]
    Component displayManagement;
    protected IDisplayManagement<T> _displayManagement;

    protected new void Awake()
    {
        base.Awake();
        _displayManagement = displayManagement.GetComponent<IDisplayManagement<T>>();
    }

    protected override void Activation(IInformable<T> obj)
    {
        _displayManagement.Display(obj.Inform());
    }

    public void Deactivate()
    {
        _displayManagement.Hide();
    }
}
