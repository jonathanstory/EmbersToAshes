using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform targetLook;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetLook);
    }
}
