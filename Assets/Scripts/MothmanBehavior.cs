using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MothmanBehavior : MonoBehaviour
{
    public Renderer objectRenderer;
    private GameObject player;
    public float damageTimer = 5f;
    private float timeSeen = 0;
    public float lifeTime = 15;
    private GameObject enemyManager;
    public Animator animator;
    private RawImage staticEffect;

    void Start()
    {
        staticEffect = GameObject.FindGameObjectWithTag("Static").GetComponent<RawImage>();
        animator.applyRootMotion = false;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (objectRenderer.isVisible)
        {
            timeSeen += Time.deltaTime;
            Debug.Log("Time Seen: " + timeSeen);

            if(staticEffect != null)
            {
                Color c = staticEffect.color;
                c.a = timeSeen/30f;
                staticEffect.color = c;
            }


            if(timeSeen > 5)
            {
                player.GetComponent<CharacterMovementSimple>().currentPlayerHealth -= 1;
                timeSeen = 0;
            }
        }
        else
        {
            timeSeen = 0;
            Color c = staticEffect.color;
            c.a = 0f;
            staticEffect.color = c;
        }

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            enemyManager.GetComponent<EnemySpawnBehavior>().DespawnEnemy(1);
            enemyManager.GetComponent<EnemySpawnBehavior>().mothmanSpawned = false;
        }

        transform.LookAt(player.transform);

        if(distanceToPlayer <= 15)
        {
            animator.SetBool("Pointing", true);
        }
        else
        {
            animator.SetBool("Pointing", false);
        }
    }

}
