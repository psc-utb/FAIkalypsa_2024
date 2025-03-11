
using InteractionSystem.Interfaces;
using TMPro;

public class TextUI : TextMeshProUGUI, IInformationActivableElement<string>
{
    public bool IsActivated => string.IsNullOrWhiteSpace(this.text) ? false : true;

    public void SetInformation(string info)
    {
        this.text = info;
    }
}
