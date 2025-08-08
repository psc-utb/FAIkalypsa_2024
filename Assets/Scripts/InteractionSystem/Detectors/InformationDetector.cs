using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationDetector<T> : Detector<IInformable<T>>
{
    protected new void Awake()
    {
        base.Awake();
    }
    public override void Detect(GameObject obj)
    {
        base.Detect(obj);
        if (obj == null)
        {
            NotifyDetected(null);
        }
    }
}
