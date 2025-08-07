using InteractionSystem.Interfaces;
using UnityEngine;

public abstract class Activator<T> : MonoBehaviour, IActivator<T>
{
    [SerializeField]
    Component detector;
    protected IDetector<T> _detector;

    protected void Awake()
    {
        _detector = detector.GetComponent<IDetector<T>>();
        _detector?.AttachDetected(Activate);
    }

    public virtual void Activate(T obj)
    {
        if (obj != null)
        {
            Activation(obj);
        }
    }

    protected abstract void Activation(T obj);
}
