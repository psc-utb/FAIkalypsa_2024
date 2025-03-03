using Assets.Scripts.Interfaces;
using UnityEngine;

public class InteractionAction : MonoBehaviour
{
    public void PerformAction(IInteractable objToInteract)
    {
        if (objToInteract != null)
        {
            objToInteract.Interact();
        }
    }

    public void ShowInteractionInfo(IInteractable objToInteract)
    {
        if (objToInteract != null)
        {
            objToInteract.ShowInteraction();
        }
    }
}
