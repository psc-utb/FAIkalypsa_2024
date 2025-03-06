
public class InformationTextActivator : InformationActivator<string>
{
    // Update is called once per frame
    protected void Update()
    {
        bool wasActive = IsActivated;
        if (Activate() == false && wasActive == true)
        {
            Deactivate();
        }
    }
}
