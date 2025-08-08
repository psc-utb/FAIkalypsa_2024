using InteractionSystem.Interfaces;
using System;
using UnityEngine;


public abstract class Detector<T1, T> : MonoBehaviour, IDetector<T>
{
    protected ISensor<T1> _sensor;

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
        _sensor?.AttachSensed(Detect);
    }

    public abstract void Detect(T1 obj);
}

public class Detector<T> : Detector<GameObject, T>, IDetector<T>
{
    [SerializeField]
    Component sensor;

    protected void Awake()
    {
        _sensor = sensor.GetComponent<ISensor<GameObject>>();
        base.Awake();
    }

    public override void Detect(GameObject obj)
    {
        if (obj != null)
        {
            var detectedObj = obj.GetComponent<T>();
            NotifyDetected(detectedObj);
        }
    }
}
