using InteractionSystem.Interfaces;
using UnityEngine;

public class SensorInputed : Sensor<GameObject>, ISensor
{
    [SerializeField]
    float maxDistanceDetection = 1f;
    public float MaxDistanceDetection { get => maxDistanceDetection; set => maxDistanceDetection = value; }

    public void Sense()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~IgnoreLayer))
        {
            SensedObject = raycastHit.collider.gameObject;
            NotifySensed(SensedObject);
        }
        else
        {
            SensedObject = null;
        }
        //return SensedObject;
    }
}
