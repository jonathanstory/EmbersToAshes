using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MothmanBehavior : MonoBehaviour
{
    public Renderer objectRenderer;
    public GameObject player;
    public float damageTimer = 5f;
    private float timeSeen = 0;

    void Start()
    {

    }

    private void Update()
    {
        if(objectRenderer.isVisible)
        {
            timeSeen += Time.deltaTime;
            Debug.Log("Time Seen: " + timeSeen);
        }
    }

}
