
using InteractionSystem.Interfaces;
using TMPro;

public class TextUI : TextMeshProUGUI, IInformationActivableElement<string>
{
    public bool IsActivated
    {
        get => string.IsNullOrWhiteSpace(this.text) ? false : true;
        set => IsActivated = value;
    }

    public void SetInformation(string info)
    {
        this.text = info;
    }
}
