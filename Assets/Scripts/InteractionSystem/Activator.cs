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

    public void Activate()
    {
        if (_detector != null)
        {
            var objectDetected = _detector.Detect<T>();
            if (objectDetected != null)
            {
                Activation(objectDetected);
                return;
            }
        }
    }

    protected abstract void Activation(T obj);
}
