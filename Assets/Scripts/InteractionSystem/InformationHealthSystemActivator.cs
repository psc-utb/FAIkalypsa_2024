using CodeMonkey.HealthSystemCM;

public class InformationHealthSystemActivator : InformationActivator<HealthSystem>
{
    // Update is called once per frame
    protected void Update()
    {
        if (Activate() == false)
        {
            Deactivate();
        }
    }
}
