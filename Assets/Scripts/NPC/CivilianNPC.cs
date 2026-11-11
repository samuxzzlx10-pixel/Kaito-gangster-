using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class CivilianNPC : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 50;
    [SerializeField, Min(1)] private int hitDamage = 8;
    [SerializeField, Min(0.1f)] private float attackRange = 1.6f;
    [SerializeField, Min(0.1f)] private float attackDelay = 1.2f;
    [SerializeField, Min(0.1f)] private float preAttackDelay = 0.35f;

    private NavMeshAgent agent;
    private Transform aggressor;
    private int currentHealth;
    private bool isAttacking;
    private float nextAttackTime;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (aggressor == null)
            return;

        float distance = Vector3.Distance(transform.position, aggressor.position);

        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(aggressor.position);
            return;
        }

        agent.isStopped = true;
        FaceTarget();

        if (!isAttacking && Time.time >= nextAttackTime)
            StartCoroutine(AttackRoutine());
    }

    public void TakeDamage(int amount, Transform source)
    {
        if (amount <= 0 || currentHealth <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        if (source != null)
            aggressor = source;

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackDelay;
        yield return new WaitForSeconds(preAttackDelay);

        if (aggressor == null)
        {
            isAttacking = false;
            yield break;
        }

        float distance = Vector3.Distance(transform.position, aggressor.position);
        if (distance <= attackRange + 0.25f)
        {
            var playerHealth = aggressor.GetComponentInParent<KaitoHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(hitDamage);
        }

        isAttacking = false;
    }

    private void FaceTarget()
    {
        if (aggressor == null) return;

        Vector3 direction = aggressor.position - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
