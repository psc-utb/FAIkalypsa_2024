using InteractionSystem.Interfaces;
using UnityEngine;

public class Sensor : MonoBehaviour, ISensor<GameObject>
{
    [SerializeField]
    float maxDistanceDetection = 1f;
    public float MaxDistanceDetection { get => maxDistanceDetection; set => maxDistanceDetection = value; }

    [SerializeField]
    LayerMask ignoreLayer;
    public LayerMask IgnoreLayer => ignoreLayer;

    public GameObject SensedObject { get; private set; }

    public GameObject Sense()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~ignoreLayer))
        {
            SensedObject = raycastHit.collider.gameObject;
        }
        else
        {
            SensedObject = null;
        }
        return SensedObject;
    }
}
