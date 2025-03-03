using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class InteractionDetector : MonoBehaviour, IInteractionDetector
{
    [SerializeField]
    float maxDistanceDetection = 1f;

    public UnityEvent<IInteractable> objectDetected;

    [SerializeField]
    LayerMask ignoreLayer;

    // Update is called once per frame
    void Update()
    {
        var objectToInteract = Detect();
        if (objectToInteract != null)
        {
            objectDetected?.Invoke(objectToInteract);
            return;
        }
        objectDetected?.Invoke(null);
    }

    public IInteractable Detect()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~ignoreLayer))
        {
            var interaction = raycastHit.collider.gameObject.GetComponent<IInteractable>();
            if (interaction != null)
            {
                return interaction;
            }
        }
        return null;
    }
}
