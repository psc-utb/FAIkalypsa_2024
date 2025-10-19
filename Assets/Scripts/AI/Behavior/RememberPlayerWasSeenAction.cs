using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RememberPlayerWasSeenAndClose", story: "[self] remembers if [playerWasSeenAndClose]", category: "Action", id: "2e0fc7e2a54291b8a7047cf1953eb644")]
public partial class RememberPlayerWasSeenAndCloseAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    [SerializeReference] public BlackboardVariable<bool> playerWasSeenAndClose;

    [SerializeReference] public BlackboardVariable<bool> playerIsVisible;
    [SerializeReference] public BlackboardVariable<float> distanceToPlayer;
    [SerializeReference] public BlackboardVariable<float> maxPlayerDistanceToChase;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || playerWasSeenAndClose == null || maxPlayerDistanceToChase == null || playerIsVisible == null || distanceToPlayer == null)
            return Status.Failure;

        if (playerIsVisible.Value == true && distanceToPlayer.Value < maxPlayerDistanceToChase.Value)
        {
            playerWasSeenAndClose.Value = true;
            return Status.Success;
        }
        else if (playerWasSeenAndClose.Value == true && distanceToPlayer.Value >= maxPlayerDistanceToChase.Value)
        {
            playerWasSeenAndClose.Value = false;
            return Status.Success;
        }

        return Status.Success;
    }

    protected override Status OnUpdate() => Status.Success;
    protected override void OnEnd() { }
}

