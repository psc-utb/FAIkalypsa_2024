using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IAttackable
{
    [SerializeField]
    HealthSystemComponent healthSystemComponent;

    public void TakeDamage(float damage)
    {
        healthSystemComponent?.GetHealthSystem().Damage(damage);
    }
}
