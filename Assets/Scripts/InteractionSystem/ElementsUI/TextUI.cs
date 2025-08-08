
using InteractionSystem.Interfaces;
using TMPro;

public class TextUI : TextMeshProUGUI, IInformationActivableElement<string>
{
    public bool IsActivated
    {
        get => string.IsNullOrWhiteSpace(this.text) ? false : true;
        set => this.gameObject.SetActive(value);
    }

    public void SetInformation(string info)
    {
        if (string.IsNullOrWhiteSpace(info))
        {
            this.text = string.Empty;
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.text = info;
            this.gameObject.SetActive(true);
        }
    }
}
