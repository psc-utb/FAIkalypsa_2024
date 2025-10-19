using System;
using CodeMonkey.HealthSystemCM;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PlayerIsAlive", story: "Tells if [player] is still alive", category: "Conditions", id: "b057128dc0ffb1f36e761363fbc43a87")]
public partial class PlayerIsAliveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    IGetHealthSystem healthSystem;

    public override bool IsTrue()
    {
        if (healthSystem == null)
            return false;
        return healthSystem.GetHealthSystem().IsDead() ? false : true;
    }

    public override void OnStart()
    {
        if (Player != null && Player.Value != null)
            healthSystem = Player.Value.GetComponent<IGetHealthSystem>();
    }

    public override void OnEnd()
    {
    }
}
