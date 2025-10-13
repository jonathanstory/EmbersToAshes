using UnityEngine;

public class ListenerPosition : MonoBehaviour
{
    public Transform newPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = newPosition.position;
    }
}
