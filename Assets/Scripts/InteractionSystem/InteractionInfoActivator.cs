using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InteractionInfoActivator : MonoBehaviour
{
    [SerializeField]
    Component interactionDetector;
    IDetector _detector;

    [SerializeField]
    protected UnityEvent<IInformable> objectWithInfoDetected;

    void Awake()
    {
        _detector = interactionDetector.GetComponent<IDetector>();
    }

    // Update is called once per frame
    void Update()
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
