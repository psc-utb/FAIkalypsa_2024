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

    public void PerformAction(GameObject objToInteract)
    {
        var interactableObject = objToInteract.GetComponent<IInteractable>();
        interactableObject.Interact();
    }

    public void ShowHideInteractionInfo(GameObject objToInteract)
    {
        if (objToInteract != null)
        {
            var interactableObject = objToInteract.GetComponent<IInteractable>();
            interactableObject.ShowInteraction();
        }
    }
}
