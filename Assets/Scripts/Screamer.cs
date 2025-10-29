using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ScreamerAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRange = 10f;           // How close the player needs to be for detection
    public float screamDuration = 7f;            // Duration of scream (should match audio + animation)

    [Header("Patrol Settings")]
    public float patrolRadius = 20f;             // Radius of map for patrolling points
    public float waitTimeAtPoint = 2f;           // Wait time before choosing next patrol point

    [Header("Flight Settings")]
    public float flightSpeed = 10f;
    public float flightHeight = 15f;             // Height mothman flies up to before flying away
    public float waitInAirTime = 3f;             // Wait time in the air before flying away

    [Header("Audio & Animation")]
    public AudioClip screamClip;                  // Screech sound
    public AudioClip landingClip;                 // Landing sound
    public Animator animator;                     // Mothman's Animator
    public AudioSource audioSource;               // AudioSource to play the sounds

    private GameObject player;
    private NavMeshAgent agent;

    private Vector3 patrolPoint;
    private bool patrolPointSet = false;

    private bool hasScreamed = false;
    private bool isFlying = false;

    private Vector3 fleeTargetPoint;

    private float waitTimer = 0f;

    // Fly phases for smooth flying logic
    private enum FlyPhase
    {
        GoingUp,
        WaitingInAir,       // NEW phase for waiting after flying up
        FlyingAway,
        GoingDown,
        Landed
    }
    private FlyPhase currentFlyPhase = FlyPhase.Landed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();

        SetRandomPatrolPoint();
    }

    void Update()
    {
        if (!isFlying)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= detectionRange)
            {
                if (!hasScreamed)
                {
                    StartCoroutine(ScreamAndFlyAway());
                }
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            FlyMovement();
        }
    }

    private void Patrol()
    {
        if (!patrolPointSet)
        {
            SetRandomPatrolPoint();
        }

        if (agent != null && patrolPointSet)
        {
            agent.SetDestination(patrolPoint);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer >= waitTimeAtPoint)
                {
                    SetRandomPatrolPoint();
                    waitTimer = 0f;
                }
            }
        }
    }

    private void SetRandomPatrolPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius + transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
            patrolPointSet = true;
        }
    }

    private IEnumerator ScreamAndFlyAway()
    {
        hasScreamed = true;
        isFlying = true;

        // Stop NavMeshAgent movement
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        // Play scream sound and animation
        if (audioSource != null && screamClip != null)
        {
            audioSource.PlayOneShot(screamClip, 0.3f); // volume lowered
        }

        if (animator != null)
        {
            animator.SetTrigger("Scream");
        }

        // Alert the Rake
        AlertRake();

        // Wait for scream duration before flying
        yield return new WaitForSeconds(screamDuration);

        // Start flying up phase
        currentFlyPhase = FlyPhase.GoingUp;
    }

    private Vector3 GetFleeTargetPoint()
    {
        Vector3 fleePoint = Vector3.zero;
        int attempts = 0;
        const int maxAttempts = 30;

        while (attempts < maxAttempts)
        {
            Vector3 randomPoint = Random.insideUnitSphere * patrolRadius + transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
            {
                if (Vector3.Distance(hit.position, player.transform.position) > detectionRange * 2)
                {
                    fleePoint = hit.position;
                    break;
                }
            }
            attempts++;
        }

        if (attempts == maxAttempts)
        {
            fleePoint = transform.position + Vector3.forward * patrolRadius;
        }

        return fleePoint;
    }

    private void FlyMovement()
    {
        float step = flightSpeed * Time.deltaTime;

        switch (currentFlyPhase)
        {
            case FlyPhase.GoingUp:
                Vector3 upTarget = new Vector3(transform.position.x, flightHeight, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, upTarget, step);

                if (Mathf.Abs(transform.position.y - flightHeight) < 0.1f)
                {
                    currentFlyPhase = FlyPhase.WaitingInAir;
                    StartCoroutine(WaitInAirCoroutine());
                }
                break;

            case FlyPhase.WaitingInAir:
                // Handled by coroutine
                break;

            case FlyPhase.FlyingAway:
                transform.position = Vector3.MoveTowards(transform.position, fleeTargetPoint, step);

                Vector3 horizontalPos = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 targetHorizontal = new Vector3(fleeTargetPoint.x, 0, fleeTargetPoint.z);

                if (Vector3.Distance(horizontalPos, targetHorizontal) < 0.1f)
                {
                    currentFlyPhase = FlyPhase.GoingDown;
                }
                break;

            case FlyPhase.GoingDown:
                Vector3 downTarget = new Vector3(fleeTargetPoint.x, 0, fleeTargetPoint.z);
                transform.position = Vector3.MoveTowards(transform.position, downTarget, step);

                if (Mathf.Abs(transform.position.y - 0) < 0.1f)
                {
                    currentFlyPhase = FlyPhase.Landed;
                    isFlying = false;

                    if (agent != null)
                    {
                        agent.enabled = true;
                        agent.isStopped = false;
                        SetRandomPatrolPoint();
                    }

                    if (animator != null)
                    {
                        animator.SetTrigger("Land");
                    }

                    if (audioSource != null && landingClip != null)
                    {
                        audioSource.PlayOneShot(landingClip);
                    }

                    StartCoroutine(ResetScreamAfterDelay(3f));
                }
                break;

            case FlyPhase.Landed:
                break;
        }
    }

    private IEnumerator WaitInAirCoroutine()
    {
        yield return new WaitForSeconds(waitInAirTime);

        fleeTargetPoint = GetFleeTargetPoint();
        fleeTargetPoint.y = flightHeight;

        currentFlyPhase = FlyPhase.FlyingAway;
    }

    private IEnumerator ResetScreamAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasScreamed = false;
    }

    // ALERT RAKE - FIXED TO WORK WITH RakeBehavior
    private void AlertRake()
    {
        GameObject rake = GameObject.Find("Rake"); // make sure GameObject is named "Rake"

        if (rake != null)
        {
            RakeBehavior rakeBehavior = rake.GetComponent<RakeBehavior>();
            if (rakeBehavior != null)
            {
                rakeBehavior.HeardSound(player.transform.position);
                Debug.Log("Rake alerted to scream at position: " + player.transform.position);
            }
            else
            {
                Debug.LogWarning("RakeBehavior component not found on Rake GameObject!");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject named 'Rake' found in the scene!");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
#endif
}
