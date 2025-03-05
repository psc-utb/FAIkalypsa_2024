using InteractionSystem.Interfaces;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InteractionDisplay : MonoBehaviour
{
    TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start()
    {
        interactionText = GetComponent<TextMeshProUGUI>();
    }

    public void Activate(string text)
    {
        interactionText.text = text;
        //interactionText.enabled = true;
    }

    public void Deactivate()
    {
        interactionText.text = string.Empty;
        //interactionText.enabled = false;
    }

    public void Deactivate(IInformable<string> objToInteract)
    {
        if (objToInteract == null)
        {
            Deactivate();
        }
    }
}
