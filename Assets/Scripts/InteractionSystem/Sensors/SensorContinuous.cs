using InteractionSystem.Interfaces;
using UnityEngine;

public class SensorContinuous : Sensor<GameObject>, ISensor
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
        }
        else
        {
            SensedObject = null;
        }
        NotifySensed(SensedObject);
        //return SensedObject;
    }

    //maybe add settings to sense not only every frame, but every second, third, fourth, fifth and so on - to increase performance.

    // Update is called once per frame
    void Update()
    {
        Sense();
    }
}
