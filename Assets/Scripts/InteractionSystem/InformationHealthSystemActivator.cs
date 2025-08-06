using CodeMonkey.HealthSystemCM;
using InteractionSystem.Interfaces;

public class InformationHealthSystemActivator : InformationActivator<HealthSystem>
{
    protected new void Awake()
    {
        base.Awake();
    }

    //// Update is called once per frame
    //protected void Update()
    //{
    //    bool wasActive = _displayManagement.IsActivated;
    //    if (Activate() == false && wasActive == true)
    //    {
    //        Deactivate();
    //    }
    //}
}
