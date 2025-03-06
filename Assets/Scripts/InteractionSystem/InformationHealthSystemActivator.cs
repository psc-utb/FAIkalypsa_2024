using CodeMonkey.HealthSystemCM;

public class InformationHealthSystemActivator : InformationActivator<HealthSystem>
{
    // Update is called once per frame
    protected void Update()
    {
        bool wasActive = _displayable.IsActivated;
        if (Activate() == false && wasActive == true)
        {
            Deactivate();
        }
    }
}
