using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
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

    public void Deactivate(IInteractable objToInteract)
    {
        if (objToInteract == null)
        {
            Deactivate();
        }
    }
}
