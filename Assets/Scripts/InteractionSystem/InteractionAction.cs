using InteractionSystem.Interfaces;
using UnityEngine;

public class InteractionAction : MonoBehaviour
{
    public void ShowInteractionInfo(IInformable<string> objWithInformation)
    {
        if (objWithInformation != null)
        {
            objWithInformation.Inform();
        }
    }
}
