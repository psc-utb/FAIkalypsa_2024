using InteractionSystem.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackActivator : Activator<IAttackable>
{
    [SerializeField]
    float damage = 10f;

    protected new void Awake()
    {
        base.Awake();
    }

    protected override void Activation(IAttackable obj)
    {
        obj.TakeDamage(damage);
    }
}
