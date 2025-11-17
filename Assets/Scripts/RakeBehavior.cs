using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class RakeBehavior : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 10f;      // How far around the enemy to pick patrol points
    public float waitTimeAtPoint = 2f;    // Time to wait at each patrol point

    [Header("Chase Settings")]
    public float sightRange = 10f;         // Distance to start chasing the player
    public float loseSightRange = 12f;     // Distance to stop chasing the player
    public float baseAvoidDistance = 10f;

    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    private bool isChasing = false;
    private bool isAlerted = false;      // True when responding to mothman's scream
    private float waitTimer = 0f;
    private float normalSpeed;
    private Vector3 alertPosition;
    private Vector3 patrolPoint;
    private bool patrolPointSet = false;
    private GameObject basePos;

    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (agent != null)
            normalSpeed = agent.speed;

        animator.applyRootMotion = false;
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        basePos = GameObject.FindGameObjectWithTag("BasePos");

        sightRange = 10f * PlayerPrefs.GetFloat("Stealth");
        loseSightRange = 12f * PlayerPrefs.GetFloat("Stealth");

        SetRandomDestination();


    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToBase = Vector3.Distance(transform.position, basePos.transform.position);

        if (!isFleeing)
        {
            // Chasing logic
            if (isChasing)
            {
                if (distanceToPlayer > loseSightRange)
                {
                    isChasing = false;
                    animator.SetBool("PlayerFound", false);
                    SetRandomDestination();
                }
                else
                {
                    agent.speed = normalSpeed * 2f; // Running speed
                    agent.SetDestination(player.position);
                }
            }
            // Alerted logic
            else if (isAlerted)
            {
                if (distanceToPlayer <= sightRange)
                {
                    isChasing = true;
                    animator.SetBool("PlayerFound", true);
                    isAlerted = false;
                    agent.speed = normalSpeed * 2f; // Running speed
                    return;
                }

                float distanceToAlertPos = Vector3.Distance(transform.position, alertPosition);

                if (distanceToAlertPos <= agent.stoppingDistance)
                {
                    isAlerted = false;
                    agent.speed = normalSpeed; // Back to walking
                    SetRandomDestination();
                }
                else
                {
                    agent.speed = normalSpeed * 2f; // Running toward alert position
                    agent.SetDestination(alertPosition);
                }
            }
            // Patrol logic
            else
            {
                if (distanceToPlayer <= sightRange)
                {
                    isChasing = true;
                    animator.SetBool("PlayerFound", true);
                }
                else
                {
                    agent.speed = normalSpeed; // Walking speed
                    animator.SetBool("PlayerFound", false);
                    Patrol();
                }
            }

            if (animator != null && agent != null)
            {
                float currentSpeed = agent.velocity.magnitude;

                if (!isChasing && currentSpeed > .2)
                    animator.SetBool("Searching", true);
                else if (!isChasing && currentSpeed <= .2)
                    animator.SetBool("Searching", false);

                if (isChasing && distanceToPlayer <= 3)
                    animator.SetBool("Attacking", true);
                else if (isChasing && distanceToPlayer > 3)
                    animator.SetBool("Attacking", false);
            }

            if (distanceToBase < 10.0f)
            {
                StartCoroutine(RunAway());
                isFleeing = true;
            }
        }
    }

    private IEnumerator RunAway()
    {
        Vector3 runPoint = (transform.position - basePos.transform.position).normalized;
        agent.SetDestination(runPoint * 10);
        yield return new WaitForSeconds(4);
        EnemySpawnBehavior.Instance.DespawnEnemy(0);
        Destroy(gameObject);
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

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            patrolPointSet = true;
            agent.SetDestination(patrolPoint);
        }
    }

    // Called by Mothman to alert the Rake
    public void HeardSound(Vector3 soundSource)
    {
        isAlerted = true;
        isChasing = false;
        alertPosition = soundSource;
        Debug.Log(name + " alerted to scream at position: " + soundSource);
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
