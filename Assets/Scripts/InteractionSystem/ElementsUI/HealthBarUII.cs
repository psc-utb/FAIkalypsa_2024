using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;

public class HealthBarUII : HealthBarUI, IInformationActivableElement<HealthSystem>
{
    public bool IsActivated
    {
        get => this.gameObject.activeSelf;
        set => this.gameObject.SetActive(value);
    }

    protected new void Start()
    {
        base.Start();
    }

    public void SetInformation(HealthSystem info)
    {
        if (info != null)
        {
            this.SetHealthSystem(info);
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
