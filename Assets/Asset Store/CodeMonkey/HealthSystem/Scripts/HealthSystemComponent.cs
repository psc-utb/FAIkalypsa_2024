using UnityEngine;
using UnityEngine.Events;

namespace CodeMonkey.HealthSystemCM {

    /// <summary>
    /// Adds a HealthSystem to a Game Object
    /// </summary>
    public class HealthSystemComponent : MonoBehaviour, IGetHealthSystem {

        [SerializeField]
        private HealthSystem healthSystem;

        public UnityEvent<GameObject> OnDeadUnity;


        private void Awake() {
            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead(object sender, System.EventArgs e)
        {
            OnDeadUnity?.Invoke(this.gameObject);
        }

        /// <summary>
        /// Get the Health System created by this Component
        /// </summary>
        public HealthSystem GetHealthSystem() {
            return healthSystem;
        }


    }

}