using System.Collections;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] resourcePrefab;
    public GameObject playerLoc;

    private Vector3 spawnPos;
    private int spawnDelay;
    private Vector3 offset;

    public bool canSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnDelay = 3;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            Instantiate(resourcePrefab[0], GetRandomSpawn(), new Quaternion(0, Random.Range(0, 359), 0, 0));
            spawnResource();
        }
    }

    private void spawnResource()
    {
        StartCoroutine(spawnTimer());
    }

    private IEnumerator spawnTimer()
    {
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    private Vector3 GetRandomSpawn()
    {
        spawnPos = GetRandomPositionAround(playerLoc.transform.position, 20);
        return spawnPos;
    }

    public Vector3 GetRandomPositionAround(Vector3 target, float distance)
    {
        // Pick a random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Get offset on the XZ plane
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        // Return world position
        return new Vector3(target.x + x, target.y, target.z + z);
    }
}
