using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckWaypointReached", story: "checks if [self] reached [currentWaypoint]", category: "Action", id: "f099a4fdc7a87f83e5f99f2d2772a08b")]
public partial class CheckWaypointReachedAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> CurrentWaypoint;

    Animator animator;
    NavMeshAgent navMeshAgent;


    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || CurrentWaypoint == null || CurrentWaypoint.Value == null)
            return Status.Failure;

        animator = Self.Value.GetComponent<Animator>();
        navMeshAgent = Self.Value.GetComponent<NavMeshAgent>();

        if (animator == null || navMeshAgent == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distanceFromWaypoint = Vector3.Distance(Self.Value.transform.position, CurrentWaypoint.Value.transform.position);
        //float distanceFromDestination = Vector3.Distance(Self.Value.transform.position, navMeshAgent.destination);
        //float distance = Mathf.Abs(distanceFromWaypoint - distanceFromDestination);
        if (distanceFromWaypoint < 0.5 /*|| distance > 1*/)
        {
            CurrentWaypoint.Value = null;
            animator.SetTrigger("Idle");
            navMeshAgent.speed = 0;
            //navMeshAgent.SetDestination(Self.Value.transform.position);
            navMeshAgent.ResetPath();

            return Status.Failure;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

