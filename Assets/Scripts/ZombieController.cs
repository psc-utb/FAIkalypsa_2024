using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ZombieController : MonoBehaviour
{

    [Tooltip("zombie speed")]
    [Range(0, 10)]
    [SerializeField]
    float speed = 1f;

    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 ||
            Input.GetAxisRaw("Vertical") != 0)
        {

            Vector3 movementX = Input.GetAxisRaw("Horizontal") * Vector3.right * speed * Time.deltaTime;
            Vector3 movementZ = Input.GetAxisRaw("Vertical") * Vector3.forward * speed * Time.deltaTime;

            Vector3 movement = movementX + movementZ;

            transform.Translate(movement);

            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
