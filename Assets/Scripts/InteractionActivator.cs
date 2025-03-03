using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class InteractionActivator : MonoBehaviour, IInteractionActivator
    {
        public UnityEvent<IInteractable> objectInteracted;

        [SerializeField]
        MonoBehaviour interactionDetector;
        IInteractionDetector _interactionDetector;

        void Awake()
        {
            _interactionDetector = interactionDetector.GetComponent<IInteractionDetector>();
        }

        public void Activate()
        {
            var objectToInteract = _interactionDetector.Detect();
            if (objectToInteract != null)
            {
                objectInteracted?.Invoke(objectToInteract);
                return;
            }
        }
    }
}
