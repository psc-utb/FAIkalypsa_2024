using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ForceSuccess", category: "Flow", id: "ad6c0198830fb94d2f3ba623ca541546")]
public partial class ForceSuccessModifier : Modifier
{

    protected override Status OnStart()
    {
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

