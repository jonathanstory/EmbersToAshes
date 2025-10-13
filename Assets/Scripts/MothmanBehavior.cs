using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MothmanBehavior : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 10f;      // How far around the enemy to pick patrol points
    public float waitTimeAtPoint = 2f;    // Time to wait at each patrol point

    [Header("Chase Settings")]
    public float sightRange = 5f;         // Distance to start chasing the player
    public float loseSightRange = 8f;     // Distance to stop chasing the player

    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    private float waitTimer = 0f;

    private Transform playerBase;
    public float avoidBaseRange = 5;
    private bool withinBase = false;
    private bool canAct = true;

    void Start()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base").transform;

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        SetRandomDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToBase = Vector3.Distance(transform.position, playerBase.transform.position);

        if ((canAct))
        {
            if (isChasing)
            {
                // When player escapes
                if (distanceToPlayer > loseSightRange)
                {
                    isChasing = false;
                    SetRandomDestination();
                }
                else
                {
                    // Continue chasing player
                    agent.SetDestination(player.position);
                }
            }
            else
            {
                // Player enters chase range
                if (distanceToPlayer <= sightRange && !withinBase)
                {
                    isChasing = true;
                }
                else
                {
                    Patrol();
                }
            }

            if (distanceToBase <= avoidBaseRange)
            {
                withinBase = true;
                RunAway();
            }
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                SetRandomDestination();
                waitTimer = 0f;
            }
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + transform.position;
        NavMeshHit hit;

        // Pick the nearest valid point on the NavMesh
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void RunAway()
    {
        canAct = false;
        Vector3 directionToRun = (transform.position - playerBase.position).normalized;
        Vector3 runVector = directionToRun * 20;
        agent.SetDestination(runVector);
        StartCoroutine(RunDeath());
    }

    private IEnumerator RunDeath()
    {
        yield return new WaitForSeconds(5);
        canAct = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, loseSightRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}
