using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WalkToWaypoint", story: "[self] walks to [currentWaypoint] with [speedWalk]", category: "Action", id: "aa2c6997e96263756de98a71197ecfb6")]
public partial class WalkToWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> CurrentWaypoint;
    [SerializeReference] public BlackboardVariable<float> SpeedWalk;

    NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || CurrentWaypoint == null || CurrentWaypoint.Value == null || SpeedWalk == null || SpeedWalk.Value == 0)
            return Status.Failure;

        navMeshAgent = Self.Value.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            if (navMeshAgent.hasPath == false || navMeshAgent.speed == 0)
            {
                navMeshAgent.SetDestination(CurrentWaypoint.Value.transform.position);
                navMeshAgent.speed = SpeedWalk.Value;

                return Status.Success;
            }
        }

        return Status.Failure;
    }

    protected override void OnEnd()
    {
    }
}

