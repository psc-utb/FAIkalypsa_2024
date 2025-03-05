using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InfoActivator : MonoBehaviour, IActivator
{
    [SerializeField]
    Component interactionDetector;
    IDetector _detector;

    [SerializeField]
    protected UnityEvent<IInformable> objectWithInfoDetected;

    protected void Awake()
    {
        _detector = interactionDetector.GetComponent<IDetector>();
    }

    // Update is called once per frame
    protected void Update()
    {
        Activate();
    }

    public void Activate()
    {
        if (_detector != null)
        {
            var objectWithInformation = _detector.Detect<IInformable>();
            if (objectWithInformation != null)
            {
                objectWithInfoDetected?.Invoke(objectWithInformation);
                return;
            }
        }
        objectWithInfoDetected?.Invoke(null);
    }
}
