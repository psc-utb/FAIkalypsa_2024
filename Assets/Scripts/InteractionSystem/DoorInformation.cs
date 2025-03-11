using System.Linq;
using InteractionSystem.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInformation : MonoBehaviour, IInformable<string>
{
    [SerializeField]
    GameObject doorWhichInforms;

    Animator _animator;
    //IOpenable doorState;


    [SerializeField]
    InputActionAsset inputActionAsset;

    void Start()
    {
        _animator = doorWhichInforms.GetComponent<Animator>();
        //doorState = doorWhichInforms.GetComponent<IOpenable>();
    }

    public string Inform()
    {
        string textWithInformation = string.Empty;

        if (/*doorState.Opened*/ _animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            var bindingKey = inputActionAsset
                            .actionMaps
                            .FirstOrDefault(inputActionMap => inputActionMap.name == "Player")
                            .actions
                            .FirstOrDefault(action => action.name == "Interaction")
                            .GetBindingDisplayString();
            textWithInformation = $"Press {bindingKey} to close";
        }

        return textWithInformation;
    }
}
