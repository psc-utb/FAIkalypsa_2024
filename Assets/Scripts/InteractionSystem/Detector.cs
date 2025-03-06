using InteractionSystem.Interfaces;
using UnityEngine;

public class Detector : MonoBehaviour, IDetector
{
    [SerializeField]
    Component sensor;
    ISensor<GameObject> _sensor;

    [SerializeField]
    bool sensorActivatedSeparately = true;
    public bool SensorActivatedSeparately { get => sensorActivatedSeparately; set => sensorActivatedSeparately = value; }

    protected void Awake()
    {
        _sensor = sensor.GetComponent<ISensor<GameObject>>();
    }

    public T Detect<T>()
    {
        if (sensorActivatedSeparately == true)
        {
            _sensor.Sense();
        }
        GameObject sensedObject = _sensor.SensedObject;
        if (sensedObject != null)
        {
            return sensedObject.GetComponent<T>();
        }
        return default(T);
    }
}
