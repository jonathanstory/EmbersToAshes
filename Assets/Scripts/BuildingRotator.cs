using UnityEngine;

public class BuildingRotator : MonoBehaviour
{

    private Transform UILookAt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UILookAt = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + UILookAt.forward);
    }
}
