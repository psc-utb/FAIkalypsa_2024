using InteractionSystem.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : Detector<IInteractable>
{
    protected new void Awake()
    {
        base.Awake();
    }
}
