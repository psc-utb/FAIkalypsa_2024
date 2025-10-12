using CodeMonkey.HealthSystemCM;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerHealthTextUI : MonoBehaviour
{
    [Tooltip("assign a reference in the Editor (which implements IGetHealthSystem)")]
    [SerializeField] private GameObject getHealthSystemGameObject;

    private HealthSystem healthSystem;

    TextMeshProUGUI textMeshProComponent;


    protected void Awake()
    {
        textMeshProComponent = GetComponent<TextMeshProUGUI>();

        if (HealthSystem.TryGetHealthSystem(getHealthSystemGameObject, out HealthSystem healthSystem))
        {
            SetHealthSystem(healthSystem);
        }
    }

    /// <summary>
    /// Set the Health System for this Health Bar
    /// </summary>
    public void SetHealthSystem(HealthSystem healthSystem)
    {
        if (this.healthSystem != null)
        {
            this.healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
        }
        this.healthSystem = healthSystem;

        UpdateHealthText();

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    /// <summary>
    /// Event fired from the Health System when Health Amount changes, update Health Bar
    /// </summary>
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        UpdateHealthText();
    }

    /// <summary>
    /// Update Health Bar using the Image fillAmount based on the current Health Amount
    /// </summary>
    private void UpdateHealthText()
    {
        textMeshProComponent.text = healthSystem.GetHealth().ToString();
    }

    /// <summary>
    /// Clean up events when this Game Object is destroyed
    /// </summary>
    protected void OnDestroy()
    {
        if (healthSystem != null)
            healthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }
}
