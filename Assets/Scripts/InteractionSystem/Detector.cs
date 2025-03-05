using InteractionSystem.Interfaces;
using UnityEngine;

public class Detector : MonoBehaviour, IDetector
{
    [SerializeField]
    float maxDistanceDetection = 1f;
    public float MaxDistanceDetection { get => maxDistanceDetection; set => maxDistanceDetection = value; }

    [SerializeField]
    LayerMask ignoreLayer;
    public LayerMask IgnoreLayer => ignoreLayer;

    public T Detect<T>()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~ignoreLayer))
        {
            return raycastHit.collider.gameObject.GetComponent<T>();
        }
        return default(T);
    }
}
