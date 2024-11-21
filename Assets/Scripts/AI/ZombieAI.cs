using CodeMonkey.HealthSystemCM;
using Ghoul.AI;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{

    Animator animator;
    NavMeshAgent navMeshAgent;

    HealthSystemComponent healthScript;


    [SerializeField]
    private GameObject player;
    public GameObject Player { get => player; }
    HealthSystemComponent playerHealthScript;


    [SerializeField]
    private GameObject Hands;


    [SerializeField]
    float speedIdle = 0;
    public float SpeedIdle { get => speedIdle; set => speedIdle = value; }

    [SerializeField]
    float speedWalk = 1.0f;
    public float SpeedWalk { get => speedWalk; set => speedWalk = value; }

    [SerializeField]
    float speedRun = 3.5f;
    public float SpeedRun { get => speedRun; set => speedRun = value; }


    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        healthScript = gameObject.GetComponent<HealthSystemComponent>();

        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        playerHealthScript = player.GetComponent<HealthSystemComponent>();
    }

    void Start()
    {
        GhoulBehaviorInit();
    }

    void GhoulBehaviorInit()
    {
        if (animator != null)
        {
            Ghoul_BehaviorBase[] targetableBehaviours = animator.GetBehaviours<Ghoul_BehaviorBase>();
            for(int i = 0; i < targetableBehaviours.Length; ++i)
            {
                targetableBehaviours[i].Player = Player;
            }

            var ghoulAttackBehavior = animator.GetBehaviours<StateMachineBehaviour>().Where(behavior => behavior is IHandable);
            var handables = ghoulAttackBehavior.ToList();
            handables.ForEach(handable => (handable as IHandable).Hands = Hands);

            
            GhoulIdleBehavior ghoulIdleBehavior = animator.GetBehaviour<GhoulIdleBehavior>();
            if (ghoulIdleBehavior != null)
            {
                ghoulIdleBehavior.Speed = speedIdle;
            }

            GhoulWalkBehavior ghoulWalkBehavior = animator.GetBehaviour<GhoulWalkBehavior>();
            if (ghoulWalkBehavior != null)
            {
                ghoulWalkBehavior.Speed = speedWalk;
            }

            GhoulRunBehavior ghoulRunBehavior = animator.GetBehaviour<GhoulRunBehavior>();
            if (ghoulRunBehavior != null)
            {
                ghoulRunBehavior.Speed = speedRun;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && healthScript.GetHealthSystem().IsDead() == false)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            animator.SetFloat("PlayerDistance", distance);


            
            //simple eyes
            Vector3 position = this.transform.position;
            position.y += 1.5f;
            Ray ray = new Ray(position, transform.TransformDirection(Vector3.forward));
            RaycastHit rayCastHit = new RaycastHit();
            Physics.Raycast(ray, out rayCastHit, 10.0f);
            //if (Physics.SphereCast(ray, 2f, out rayCastHit, 10))
            if (rayCastHit.collider != null && rayCastHit.collider.gameObject == player)
            {
                animator.SetBool("PlayerIsVisible", true);
            }
            Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * rayCastHit.distance, Color.yellow);
            


            bool playerIsDead = false;
            if (playerHealthScript != null)
            {
                playerIsDead = playerHealthScript.GetHealthSystem().IsDead();
            }
            animator.SetBool("PlayerIsAlive", !playerIsDead);

        }
    }


    public void PlayerVisible(bool isVisible)
    {
        animator.SetBool("PlayerIsVisible", isVisible);
    }

}