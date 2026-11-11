using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField, Min(0.1f)] private float detectionRange = 15f;
    [SerializeField, Min(0.1f)] private float attackRange = 2.5f;
    [SerializeField] private bool alwaysChase = true;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool shouldChase = distance <= detectionRange || alwaysChase;

        if (shouldChase)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                // Placeholder combat hook. Production can call attack logic here.
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
