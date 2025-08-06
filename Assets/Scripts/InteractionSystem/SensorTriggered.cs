using UnityEngine;

public class SensorTriggered : Sensor<GameObject>
{
    public override void Sense()
    {
        //SensedObject;
        OnTriggerEnter(null);
    }

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
