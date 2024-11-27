using Assets.Scripts.Interfaces;
using UnityEngine;

public class InteractionAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
