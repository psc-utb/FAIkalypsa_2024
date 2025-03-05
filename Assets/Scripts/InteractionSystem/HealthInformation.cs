using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;
using UnityEngine;

public class HealthInformation : MonoBehaviour, IInformable<HealthSystem>
{
    [SerializeField]
    Component healthSystem;
    IGetHealthSystem _healthSystem;

    void Awake()
    {
        _healthSystem = healthSystem.GetComponent<IGetHealthSystem>();
    }

    public HealthSystem Inform()
    {
        HealthSystem healthSystem = null;
        if (_healthSystem != null)
        {
            healthSystem = _healthSystem.GetHealthSystem();
        }
        return healthSystem;
    }
}
