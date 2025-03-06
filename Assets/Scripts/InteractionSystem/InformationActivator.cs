using InteractionSystem.Interfaces;
using UnityEngine;

public class InformationActivator<T> : Activator<IInformable<T>>, IDeactivator
{
    [SerializeField]
    Component displayable;
    protected IDisplayable<T> _displayable;

    [SerializeField]
    Component hideable;
    protected IHideable _hideable;

    protected new void Awake()
    {
        base.Awake();
        _displayable = displayable.GetComponent<IDisplayable<T>>();
        _hideable = hideable.GetComponent<IHideable>();
    }

    protected override void Activation(IInformable<T> obj)
    {
        _displayable.Display(obj.Inform());
    }

    public void Deactivate()
    {
        _hideable.Hide();
    }
}
