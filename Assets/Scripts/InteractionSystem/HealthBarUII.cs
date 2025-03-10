using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;

public class HealthBarUII : HealthBarUI, IInformationElement<HealthSystem>
{
    public bool IsActivated => this.gameObject.activeSelf;

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
