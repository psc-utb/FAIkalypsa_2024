using InteractionSystem.Interfaces;
using UnityEngine;

public abstract class DisplayBase<T> : MonoBehaviour, IDisplayManagement<T>
{
    [SerializeField]
    Component UIComponent;
    protected IInformationElement<T> infoElement;

    protected void Awake()
    {
        infoElement = UIComponent.GetComponent<IInformationElement<T>>();
    }

    public virtual bool IsActivated { get; protected set; }

    public abstract void Display(T obj);
    public abstract void Hide();
}
