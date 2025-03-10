using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;
using UnityEngine;

public class HealthSystemDisplay : MonoBehaviour, IDisplayManagement<HealthSystem>
{
    [SerializeField]
    GameObject healthBarUI;

    public bool IsActivated => healthBarUI.activeSelf;

    public void Display(HealthSystem obj)
    {
        if (obj != null && obj.IsDead() == false && healthBarUI != null)
        {
            var healthBarScript = healthBarUI.GetComponent<HealthBarUI>();
            healthBarScript.SetHealthSystem(obj);
            healthBarUI.SetActive(true);
        }
        else
        {
            Hide();
        }
    }

    public void Hide()
    {
        healthBarUI?.SetActive(false);
    }
}
