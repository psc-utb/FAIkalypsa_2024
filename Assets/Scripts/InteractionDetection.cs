using Assets.Scripts.Interfaces;
using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionDetection : MonoBehaviour
{
    [SerializeField]
    float maxDistanceDetection = 1f;

    public UnityEvent<IInteractable> objectDetected;
    public UnityEvent<IInteractable> objectInteracted;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var objectToInteract = DetectInteraction();
        if (objectToInteract != null)
        {
            objectDetected?.Invoke(objectToInteract);
            return;
        }
        objectDetected?.Invoke(null);
    }

    public IInteractable DetectInteraction()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~LayerMask.GetMask("Player")))
        {
            var interaction = raycastHit.collider.gameObject.GetComponent<IInteractable>();
            if (interaction != null)
            {
                return interaction;
            }
        }
        return null;
    }

    public void InvokeInteraction()
    {
        var objectToInteract = DetectInteraction();
        if (objectToInteract != null)
        {
            objectInteracted?.Invoke(objectToInteract);
            return;
        }
    }
}
