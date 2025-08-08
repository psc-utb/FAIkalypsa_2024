using InteractionSystem.Interfaces;
using System;
using UnityEngine;

public abstract class Sensor<T> : MonoBehaviour, ISensor<T>
{
    [SerializeField]
    LayerMask ignoreLayer;
    public LayerMask IgnoreLayer => ignoreLayer;

    public T SensedObject { get; protected set; }

    Action<T> callback;

    public void AttachSensed(Action<T> callback)
    {
        this.callback += callback;
    }

    protected void NotifySensed(T obj)
    {
        this.callback?.Invoke(obj);
    }
}
