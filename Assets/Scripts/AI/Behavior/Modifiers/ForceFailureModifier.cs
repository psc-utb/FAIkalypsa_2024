using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ForceFailure", category: "Flow", id: "ba2565b8386653a3690f0ce5b3b6a0b7")]
public partial class ForceFailureModifier : Modifier
{

    protected override Status OnStart()
    {
        return Status.Failure;
    }

    protected override Status OnUpdate()
    {
        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

