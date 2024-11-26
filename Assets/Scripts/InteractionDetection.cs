using Assets.Scripts.Interfaces;
using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionDetection : MonoBehaviour
{
    [SerializeField]
    float maxDistanceDetection = 0.5f;

    public UnityEvent<GameObject> objectDetected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~LayerMask.GetMask("Player")))
        {
            var interaction = raycastHit.collider.gameObject.GetComponent<IInteractable>();
            if (interaction != null)
            {
                objectDetected?.Invoke(raycastHit.collider.gameObject);
            }
            else
            {
                objectDetected?.Invoke(null);
            }
        }
    }
}
