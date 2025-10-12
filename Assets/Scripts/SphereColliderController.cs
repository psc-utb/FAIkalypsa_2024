using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SphereColliderController : MonoBehaviour
{
    [SerializeField]
    float colliderRadius = 0.1f;
    [SerializeField]
    float colliderPositionY = 1;

    SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void Dead(GameObject go)
    {
        _collider.radius = colliderRadius;
        _collider.center = new Vector3(0, colliderPositionY, 0);
    }
}
