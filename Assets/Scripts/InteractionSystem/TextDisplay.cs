
public class TextDisplay : DisplayBase<string>
{
    public override void Display(string text)
    {
        infoElement.SetInformation(text);
        IsActivated = true;
    }

    public override void Hide()
    {
        infoElement.SetInformation(string.Empty);
        IsActivated = false;
    }
}
