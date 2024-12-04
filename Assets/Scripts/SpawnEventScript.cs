using System;
using SOLID_Object_Pool;
using UnityEngine;

public class SpawnEventScript : MonoBehaviour, ISpawnTriggerable
{
    public event Action SpawnTrigger;

    /*[SerializeField]
    private GameObject CollisionTriggerGO;*/

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        //if (collider.gameObject == CollisionTriggerGO)
            SpawnTrigger?.Invoke();
    }
}
