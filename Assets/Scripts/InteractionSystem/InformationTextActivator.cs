using InteractionSystem.Interfaces;
using UnityEngine;
//using UnityEngine.Events;

public class InformationTextActivator : Activator<IInformable<string>>
{
    [SerializeField]
    Component displayable;
    IDisplayable<string> _displayable;

    [SerializeField]
    Component hideable;
    IHideable _hideable;

    /*[SerializeField]
    protected UnityEvent<IInformable<string>> objectActivated;*/

    protected new void Awake()
    {
        base.Awake();
        _displayable = displayable.GetComponent<IDisplayable<string>>();
        _hideable = hideable.GetComponent<IHideable>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Activate() == false)
        {
            _hideable.Hide();
            //objectActivated?.Invoke(null);
        }
    }

    protected override void Activation(IInformable<string> obj)
    {
        _displayable.Display(obj.Inform());
        //objectActivated?.Invoke(obj);
    }
}
