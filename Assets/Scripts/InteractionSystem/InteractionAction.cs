using InteractionSystem.Interfaces;
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

    public void ShowInteractionInfo(IInformable objWithInformation)
    {
        if (objWithInformation != null)
        {
            objWithInformation.Inform();
        }
    }
}
