using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackPlayerAction", story: "[Self] attacks [player] using [hands]", category: "Action", id: "40f91fb952ffb8bbb0fa39a4a9fe70e1")]
public partial class AttackPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<GameObject> Hands;

    Animator animator;

    protected override Status OnStart()
    {
        if (Self == null || Self.Value == null || Player == null || Player.Value == null || Hands == null || Hands.Value == null)
        {
            return Status.Failure;
        }

        animator = Self.Value.GetComponent<Animator>();
        if (animator == null)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // logic during attack - wait until the Attack animation is performed
        string stateName = "Attack1";
        var currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        var nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);
        if ((currentAnimatorStateInfo.IsName(stateName) || nextAnimatorStateInfo.IsName(stateName)) && Hands.Value.activeSelf == false)
        {
            Hands.Value.SetActive(true);

            return Status.Running;
        }
        else if (nextAnimatorStateInfo.IsName(stateName) || currentAnimatorStateInfo.IsName(stateName) && currentAnimatorStateInfo.normalizedTime < 1)
        {
            return Status.Running;
        }

        return Status.Success;
    }

    protected override void OnEnd()
    {
        if (Hands != null && Hands.Value != null)
        {
            Hands.Value.SetActive(false);
        }
    }
}

