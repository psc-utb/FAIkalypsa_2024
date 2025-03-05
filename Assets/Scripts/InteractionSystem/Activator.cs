using InteractionSystem.Interfaces;
using UnityEngine;

public abstract class Activator<T> : MonoBehaviour, IActivator
{
    [SerializeField]
    Component interactionDetector;
    IDetector _detector;

    protected void Awake()
    {
        _detector = interactionDetector.GetComponent<IDetector>();
    }

    public bool Activate()
    {
        if (_detector != null)
        {
            var objectDetected = _detector.Detect<T>();
            if (objectDetected != null)
            {
                Activation(objectDetected);
                return true;
            }
        }
        return false;
    }

    protected abstract void Activation(T obj);
}
