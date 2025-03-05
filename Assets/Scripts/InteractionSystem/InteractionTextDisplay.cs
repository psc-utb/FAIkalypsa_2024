using InteractionSystem.Interfaces;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InteractionTextDisplay : MonoBehaviour, IDisplayable<string>, IHideable
{
    TextMeshProUGUI interactionText;

    [SerializeField]
    Component interactionTextComponent;
    //ITextable _textObject;

    // Start is called before the first frame update
    void Start()
    {
        interactionText = GetComponent<TextMeshProUGUI>();
    }

    public void Display(string text)
    {
        interactionText.text = text;
        //interactionText.enabled = true;
    }

    public void Hide()
    {
        interactionText.text = string.Empty;
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
