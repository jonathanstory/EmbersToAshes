using System.Collections;
using UnityEngine;

public class EnemySpawnBehavior : MonoBehaviour
{
    public GameObject enemyToSpawnPrefab;
    public GameObject playerLoc;
    public GameObject baseLoc;
    private GameObject spawnedEnemy;

    private Vector3 spawnPos;
    private Vector3 checkValidSpawn;
    private int spawnDelay;
    private Vector3 offset;

    public bool canSpawn;

    public static EnemySpawnBehavior Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnDelay = 10;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    { 
        if (canSpawn && GameManager.Instance.timeSurvived >= 15) // Time since round start in seconds
        {
            spawnEnemy();
            canSpawn = false;
        }


        if (spawnedEnemy != null)
        {
            if (Vector3.Distance(playerLoc.transform.position, spawnedEnemy.transform.position) > 30)
            {
                DespawnEnemy(spawnedEnemy);
            }
        }
    }

    private void spawnEnemy()
    {
        StartCoroutine(spawnTimer());
    }

    private IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(spawnDelay);

        checkValidSpawn = GetRandomPositionAround(playerLoc.transform.position, 20);

        while(Vector3.Distance(checkValidSpawn, baseLoc.transform.position) < 10)
        {
            checkValidSpawn = GetRandomPositionAround(playerLoc.transform.position, 25);
        }

        spawnPos = checkValidSpawn;
        spawnedEnemy = Instantiate(enemyToSpawnPrefab, spawnPos, Quaternion.identity);
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

    public void DespawnEnemy(GameObject enemy)
    {
        Destroy(enemy);
        spawnedEnemy = null;
        canSpawn = true;
    }

}
