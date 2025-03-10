using CodeMonkey.HealthSystemCM;

public class HealthSystemDisplay : DisplayBase<HealthSystem>
{
    public override bool IsActivated => infoElement.IsActivated;

    public override void Display(HealthSystem obj)
    {
        if (obj != null && obj.IsDead() == false && infoElement != null)
        {
            infoElement.SetInformation(obj);
        }
        else
        {
            Hide();
        }
    }

    public override void Hide()
    {
        infoElement.SetInformation(null);
    }
}
