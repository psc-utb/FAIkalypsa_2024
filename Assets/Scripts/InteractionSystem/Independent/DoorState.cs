
using UnityEngine;

public class DoorState : MonoBehaviour, IOpenable
{
    [SerializeField]
    bool opened = false;
    public bool Opened { get => opened; set => opened = value; }
}

