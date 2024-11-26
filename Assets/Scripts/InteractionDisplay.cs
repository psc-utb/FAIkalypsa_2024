using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class InteractionDisplay : MonoBehaviour
{
    TextMeshPro interactionText;

    // Start is called before the first frame update
    void Start()
    {
        interactionText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(string text)
    {
        interactionText.text = text;
        interactionText.enabled = true;
    }

    public void Deactivate()
    {
        interactionText.text = string.Empty;
        interactionText.enabled = false;
    }
}
