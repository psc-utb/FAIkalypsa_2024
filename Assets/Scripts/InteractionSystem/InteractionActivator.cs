using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InteractionActivator : MonoBehaviour, IInteractionActivator
{
    [SerializeField]
    Component interactionDetector;
    IDetector _detector;

    [SerializeField]
    protected UnityEvent<IInteractable> objectInteracted;

    void Awake()
    {
        _detector = interactionDetector.GetComponent<IDetector>();
    }

    public void Activate()
    {
        var objectToInteract = _detector.Detect<IInteractable>();
        if (objectToInteract != null)
        {
            objectInteracted?.Invoke(objectToInteract);
            return;
        }
    }
}
