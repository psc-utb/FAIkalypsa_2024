using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ResetNavMeshAgent", story: "resets NavMeshAgent from [self] and sets [speedIdle]", category: "Action", id: "b9d333c31e8f2a848c9c0bb1b322f0bb")]
public partial class ResetNavMeshAgentAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> SpeedIdle;

    NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || SpeedIdle == null)
            return Status.Failure;

        navMeshAgent = Self.Value.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (navMeshAgent.hasPath && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.ResetPath();
        }
        navMeshAgent.speed = SpeedIdle.Value;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

