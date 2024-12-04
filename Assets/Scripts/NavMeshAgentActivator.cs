using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentActivator : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Activate()
    {
        navMeshAgent.enabled = true;
    }

    public void Deactivate()
    {
        navMeshAgent.baseOffset = -2;
        navMeshAgent.ResetPath();
        navMeshAgent.speed = 0;
        navMeshAgent.enabled = false;
    }
}
