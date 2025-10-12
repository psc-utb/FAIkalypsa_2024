using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CapsuleColliderController : MonoBehaviour
{
    [SerializeField]
    float colliderRadius = 0.1f;
    [SerializeField]
    float colliderHeight = 0.1f;

    CapsuleCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    public void Dead(GameObject go)
    {
        _collider.radius = colliderRadius;
        _collider.height = colliderHeight;
    }
}
