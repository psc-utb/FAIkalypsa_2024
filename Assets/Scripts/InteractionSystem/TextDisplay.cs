using InteractionSystem.Interfaces;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextDisplay : MonoBehaviour, IDisplayManagement<string>
{
    TextMeshProUGUI interactionText;

    [SerializeField]
    Component interactionTextComponent;

    public bool IsActivated { get; private set; }

    //ITextable _textObject;

    // Start is called before the first frame update
    void Start()
    {
        interactionText = GetComponent<TextMeshProUGUI>();
    }

    public void Display(string text)
    {
        interactionText.text = text;
        IsActivated = true;
        //interactionText.enabled = true;
    }

    public void Hide()
    {
        interactionText.text = string.Empty;
        IsActivated = false;
        //interactionText.enabled = false;
    }

    public void Hide(IInformable<string> objToInteract)
    {
        if (objToInteract == null)
        {
            Hide();
        }
    }
}
