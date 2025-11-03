using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MothmanBehavior : MonoBehaviour
{
    public Renderer objectRenderer;
    public GameObject player;
    public float damageTimer = 5f;
    private float timeSeen = 0;
    public float lifeTime = 15;
    private GameObject enemyManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
    }

    private void Update()
    {
        if(objectRenderer.isVisible)
        {
            timeSeen += Time.deltaTime;
            Debug.Log("Time Seen: " + timeSeen);
            
            if(timeSeen > 5)
            {
                player.GetComponent<CharacterMovementSimple>().currentPlayerHealth -= 1;
                timeSeen = 0;
            }
        }
        else
        {
            timeSeen = 0;
        }

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            enemyManager.GetComponent<EnemySpawnBehavior>().DespawnEnemy(1);
            enemyManager.GetComponent<EnemySpawnBehavior>().mothmanSpawned = false;
        }

        transform.LookAt(player.transform);
    }

}
