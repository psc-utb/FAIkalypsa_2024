using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TransformIsNotSetCondition", story: "[currentWaypoint] is not set", category: "Conditions", id: "a465695e210cb73a0a513ea58acb332b")]
public partial class TransformIsNotSetCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> CurrentWaypoint;

    public override bool IsTrue()
    {
        return CurrentWaypoint == null || CurrentWaypoint.Value == null;
    }
}
