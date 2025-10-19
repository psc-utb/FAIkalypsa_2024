using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Assets._Scripts;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetWaypoint", story: "Set [currentWaypoint] for [self]", category: "Action", id: "15a7be7c829e629d39c6996408f6ed72")]
public partial class SetWaypointAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> CurrentWaypoint;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private Waypoints wayPoints;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || CurrentWaypoint == null)
            return Status.Failure;

        wayPoints = Self.Value.GetComponent<Waypoints>();

        if (wayPoints == null)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (CurrentWaypoint.Value == null)
        {
            float distanceFromWaypoint;
            do
            {
                int index = UnityEngine.Random.Range(0, wayPoints.WayPoints.Length);
                CurrentWaypoint.Value = wayPoints.WayPoints[index].transform;

                distanceFromWaypoint = Vector3.Distance(Self.Value.transform.position, CurrentWaypoint.Value.transform.position);
            } while (distanceFromWaypoint < 0.5);
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

