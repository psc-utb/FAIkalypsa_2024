using CodeMonkey.HealthSystemCM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemDetection : MonoBehaviour
{
    [SerializeField]
    float maxDistanceDetection = 20;

    [SerializeField]
    GameObject healthBarUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceDetection, ~LayerMask.GetMask("Player")))
        {
            var healthSysComp = raycastHit.collider.gameObject.GetComponent<HealthSystemComponent>();
            if (healthSysComp != null && healthBarUI != null)
            {
                var healthBarScript = healthBarUI.GetComponent<HealthBarUI>();
                healthBarScript.SetHealthSystem(healthSysComp.GetHealthSystem());
                healthBarUI.SetActive(true);
            }
            else
            {
                healthBarUI?.SetActive(false);
            }
        }
        else
        {
            healthBarUI?.SetActive(false);
        }
    }
}
