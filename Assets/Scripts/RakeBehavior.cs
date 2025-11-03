using UnityEngine;
using UnityEngine.AI;

public class RakeBehavior : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float patrolRadius = 10f;      // How far around the enemy to pick patrol points
    public float waitTimeAtPoint = 2f;    // Time to wait at each patrol point

    [Header("Chase Settings")]
    public float sightRange = 5f;         // Distance to start chasing the player
    public float loseSightRange = 8f;     // Distance to stop chasing the player

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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (agent != null)
            normalSpeed = agent.speed;

        SetRandomDestination();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Chasing logic
        if (isChasing)
        {
            if (distanceToPlayer > loseSightRange)
            {
                isChasing = false;
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
            }
            else
            {
                agent.speed = normalSpeed; // Walking speed
                Patrol();
            }
        }

        // Update walking/running animation based on agent velocity
        if (animator != null && agent != null)
        {
            float currentSpeed = agent.velocity.magnitude;

            // Smoothly update Speed parameter (for blend tree)
            animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);

            // Optional booleans for separate animations
            animator.SetBool("IsRunning", currentSpeed > normalSpeed * 1.1f);
            animator.SetBool("IsWalking", currentSpeed > 0.1f && currentSpeed <= normalSpeed * 1.1f);
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
