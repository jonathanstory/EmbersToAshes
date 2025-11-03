using System.Collections;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject playerLoc;
    public GameObject baseLoc;
    public GameObject[] spawnedEnemy = new GameObject[2];

    private Vector3 spawnPos;
    private Vector3 checkValidSpawn;
    private int spawnDelay;
    private Vector3 offset;
    public GameObject basePos;

    public bool rakeSpawned = false;
    public bool canSpawnRake = false;
    public bool canSpawnMothman = false;
    public bool mothmanSpawned = false;
    public bool canSpawnScreamer = false;
    public bool screamerSpawned = false;

    public static EnemySpawnBehavior Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnDelay = 10;
    }

    // Update is called once per frame
    void Update()
    { 
        if(GameManager.Instance.timeSurvived >= 15)
        {
            canSpawnRake = true;
        }

        if(GameManager.Instance.timeSurvived >= 30)
        {
            canSpawnMothman = true;
        }


        if (GameManager.Instance.timeSurvived >= 45)
        {
            canSpawnScreamer = true;
        }

        if (canSpawnRake && !rakeSpawned) // Time since round start in seconds
        {
            rakeSpawned = true;
            spawnRake();
        }

        if (canSpawnMothman && !mothmanSpawned)
        {
            mothmanSpawned = true;
            spawnMothman();
        }

        if (canSpawnScreamer && !screamerSpawned)
        {
            screamerSpawned = true;
            spawnScreamer();
        }


        if (spawnedEnemy[0] != null)
        {
            if (Vector3.Distance(playerLoc.transform.position, spawnedEnemy[0].transform.position) > 30)
            {
                DespawnEnemy(0);
                rakeSpawned = false;
            }

            if(Vector3.Distance(spawnedEnemy[0].transform.position, basePos.transform.position) < 6)
            {
                DespawnEnemy(0);
                rakeSpawned = false;
            }
        }
    }

    private void spawnRake()
    {
        StartCoroutine(spawnTimer(0));
    }

    private void spawnMothman()
    {
        StartCoroutine(spawnTimer(1));
    }

    private void spawnScreamer()
    {
        StartCoroutine(spawnTimer(2));
    }

    private IEnumerator spawnTimer(int enemyType)
    {
        yield return new WaitForSeconds(spawnDelay);

        checkValidSpawn = GetRandomPositionAround(playerLoc.transform.position, 25);

        while(Vector3.Distance(checkValidSpawn, baseLoc.transform.position) < 12)
        {
            checkValidSpawn = GetRandomPositionAround(playerLoc.transform.position, 30);
        }

        spawnPos = checkValidSpawn;
        spawnedEnemy[enemyType] = Instantiate(enemies[enemyType], spawnPos, Quaternion.identity);
    }

    public Vector3 GetRandomPositionAround(Vector3 target, float distance)
    {
        // Pick a random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Get offset on the XZ plane
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        Vector3 returnPos = new Vector3(target.x + x, target.y, target.z + z);

        // Return world position
        return returnPos;
    }

    public void DespawnEnemy(int enemy)
    {
        Destroy(spawnedEnemy[enemy]);
        spawnedEnemy[enemy] = null;
    }

}
