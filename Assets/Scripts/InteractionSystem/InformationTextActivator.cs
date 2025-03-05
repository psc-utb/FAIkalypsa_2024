
public class InformationTextActivator : InformationActivator<string>
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
