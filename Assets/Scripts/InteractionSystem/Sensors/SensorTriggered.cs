using UnityEngine;

public class SensorTriggered : Sensor<GameObject>
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            SensedObject = other.gameObject;
        }
        else
        {
            SensedObject = null;
        }
        NotifySensed(SensedObject);
    }
}
