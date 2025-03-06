using InteractionSystem.Interfaces;
using UnityEngine;

public abstract class Activator<T> : MonoBehaviour, IActivator
{
    [SerializeField]
    Component detector;
    IDetector _detector;

    public bool IsActivated { get; private set;}

    protected void Awake()
    {
        _detector = detector.GetComponent<IDetector>();
    }

    public bool Activate()
    {
        if (_detector != null)
        {
            var objectDetected = _detector.Detect<T>();
            if (objectDetected != null)
            {
                Activation(objectDetected);
                IsActivated = true;
                return IsActivated;
            }
        }
        IsActivated = false;
        return IsActivated;
    }

    protected abstract void Activation(T obj);
}
