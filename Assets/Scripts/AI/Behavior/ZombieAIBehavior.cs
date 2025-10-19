using Assets._Scripts;
using CodeMonkey.HealthSystemCM;
using Ghoul.AI;
using SOLID_Object_Pool;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAIBehavior : MonoBehaviour, IDespawnable<GameObject>, IReinitializable
{

    Animator animator;
    NavMeshAgent navMeshAgent;

    BehaviorGraphAgent behaviorGraphAgent;

    HealthSystemComponent healthScript;


    [SerializeField]
    private GameObject player;
    public GameObject Player { get => player; }
    HealthSystemComponent playerHealthScript;



    CapsuleCollider capsule;

    float capsuleHeight;
    Vector3 capsuleCenter;


    public event Action<GameObject, bool> Despawn;


    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        healthScript = gameObject.GetComponent<HealthSystemComponent>();

        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        behaviorGraphAgent = gameObject.GetComponent<BehaviorGraphAgent>();

        playerHealthScript = player.GetComponent<HealthSystemComponent>();


        capsule = gameObject.GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && healthScript.GetHealthSystem().IsDead() == false)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            behaviorGraphAgent.SetVariableValue("distanceToPlayer", distance);




            //simple eyes
            Vector3 position = this.transform.position;
            position.y += 1.5f;
            Ray ray = new Ray(position, transform.TransformDirection(Vector3.forward));
            RaycastHit rayCastHit = new RaycastHit();
            Physics.Raycast(ray, out rayCastHit, 10.0f);
            //if (Physics.SphereCast(ray, 2f, out rayCastHit, 10))
            if (rayCastHit.collider != null && rayCastHit.collider.gameObject == player)
            {
                behaviorGraphAgent.SetVariableValue("playerIsVisible", true);
            }
            Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * rayCastHit.distance, Color.yellow);
            


            bool playerIsDead = false;
            if (playerHealthScript != null)
            {
                playerIsDead = playerHealthScript.GetHealthSystem().IsDead();
            }
            behaviorGraphAgent.SetVariableValue("playerIsAlive", !playerIsDead);

        }
    }


    public void PlayerVisible(bool isVisible)
    {
        behaviorGraphAgent.SetVariableValue("playerIsVisible", isVisible);
    }


    public void Dead(GameObject deadGameObject)
    {
        animator.SetTrigger("Death");
        behaviorGraphAgent.SetVariableValue("GhoulIsAlive", false);

        Despawn?.Invoke(this.gameObject, true);
    }


    public void Reinitializate()
    {
        healthScript.GetHealthSystem().SetHealth(healthScript.GetHealthSystem().GetHealthMax());

        capsule.height = capsuleHeight;
        capsule.center = capsuleCenter;

        navMeshAgent.baseOffset = 0;
        navMeshAgent.enabled = true;

        //just to remove warning in Unity editor about animator setting while gameobject is inactive
        gameObject.SetActive(true);


        animator.ResetTrigger("Death");
        //animator.SetTrigger("Idle");
        behaviorGraphAgent.SetVariableValue("GhoulIsAlive", true);

        behaviorGraphAgent.SetVariableValue("playerIsVisible", false);

        behaviorGraphAgent.SetVariableValue("distanceToPlayer", float.PositiveInfinity);

        behaviorGraphAgent.SetVariableValue("playerIsAlive", !playerHealthScript.GetHealthSystem().IsDead());

        animator.Play("Idle", 0, 0);


    }
}
