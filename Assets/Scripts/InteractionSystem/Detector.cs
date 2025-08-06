using InteractionSystem.Interfaces;
using System;
using UnityEngine;

public class Detector<T> : MonoBehaviour, IDetector<T, GameObject>
{
    [SerializeField]
    Component sensor;
    ISensor<GameObject> _sensor;

    Action<T> callback;

    public void AttachDetected(Action<T> callback)
    {
        this.callback += callback;
    }

    protected void NotifyDetected(T obj)
    {
        callback?.Invoke(obj);
    }

    protected void Awake()
    {
        _sensor = sensor.GetComponent<ISensor<GameObject>>();
        _sensor?.AttachSensed(Detect);
    }

    //private void OnSensedObjectDetected(GameObject obj)
    //{
    //    if (obj != null)
    //    {
    //        var detectedObj = Detect();
    //        if (detectedObj != null)
    //        {
    //            NotifyDetected(detectedObj);
    //        }
    //    }
    //}

    public virtual void Detect(GameObject obj)
    {
        if (obj != null)
        {
            var detectedObj = obj.GetComponent<T>();
            NotifyDetected(detectedObj);
        }
    }
}
