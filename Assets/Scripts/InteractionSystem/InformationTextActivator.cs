
public class InformationTextActivator : InformationActivator<string>
{
    // Update is called once per frame
    protected void Update()
    {
        bool wasActive = _displayManagement.IsActivated;
        if (Activate() == false && wasActive == true)
        {
            Deactivate();
        }
    }
}
