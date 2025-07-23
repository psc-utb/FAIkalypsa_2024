
using InteractionSystem.Interfaces;
using TMPro;

public class TextUI : TextMeshProUGUI, IInformationActivableElement<string>
{
    private bool isActivated;
    public bool IsActivated
    {
        get => string.IsNullOrWhiteSpace(this.text) || isActivated == false ? false : true;
        set => isActivated = value;
    }

    public void SetInformation(string info)
    {
        this.text = info;
    }
}
