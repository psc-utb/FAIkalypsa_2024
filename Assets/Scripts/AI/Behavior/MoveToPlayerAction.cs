using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using CodeMonkey.HealthSystemCM;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToPlayerAction", story: "[self] chase [player] with [speedRun] if [playerIsAlive] and [distanceToPlayer] is in range", category: "Action", id: "7c5ea8374a2eb908f220028033a307d6")]
public partial class MoveToPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> SpeedRun;
    [SerializeReference] public BlackboardVariable<bool> PlayerIsAlive;

    [SerializeReference] public BlackboardVariable<float> DistanceToPlayer;
    [SerializeReference] public BlackboardVariable<float> maxPlayerDistanceToAttack;

    [SerializeReference] public BlackboardVariable<bool> playerWasSeenAndClose;

    [SerializeReference] public BlackboardVariable<bool> playerIsVisible;

    NavMeshAgent navMeshAgent;

    protected override Status OnStart()
    {

        if (Self == null || Self.Value == null || Player == null || Player.Value == null || SpeedRun == null || SpeedRun.Value == 0)
            return Status.Failure;

        navMeshAgent = Self.Value.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            return Status.Failure;

        if (DistanceToAttack() && playerIsVisible || PlayerIsAlive == false)
            return Status.Success;

        if (playerWasSeenAndClose == false)
            return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(Player.Value.transform.position);
            navMeshAgent.speed = SpeedRun.Value;
        }

        if (DistanceToAttack() && playerIsVisible || PlayerIsAlive == false)
            return Status.Success;

        if (playerWasSeenAndClose == false)
            return Status.Failure;


        return Status.Running;
    }

    private bool DistanceToAttack()
    {
        return DistanceToPlayer < maxPlayerDistanceToAttack.Value/* || DistanceToPlayer <= navMeshAgent.stoppingDistance*/;
    }

    protected override void OnEnd()
    {
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.speed = 0;
            //navMeshAgent.SetDestination(Self.Value.transform.position);
            navMeshAgent.ResetPath();
        }
    }
}

