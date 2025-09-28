using UnityEngine;

public class StickCollector : MonoBehaviour
{
    public int sticksHave = 0;
    public float pickupDistance = 1.5f; // How close player must be
    public GameObject[] sticks; // Assign all sticks in the scene
    public Campfire campfire;
    public float interactDistance = 3f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && sticksHave > 0)
        {
            float distance = Vector3.Distance(transform.position, campfire.transform.position);
            if (distance <= interactDistance)
            {
                campfire.AddSticks(sticksHave);
                sticksHave = 0;
                Debug.Log("Added Firewood to the campfire!"); 
            }        
        }
        foreach (GameObject stick in sticks)
            {
                if (stick != null)
                {
                    float distance = Vector3.Distance(transform.position, stick.transform.position);
                    if (distance <= pickupDistance)
                    {
                        sticksHave++;
                        Destroy(stick);
                        Debug.Log("Collected FireWood (" + sticksHave + ")");
                    }
                }
            }
    }
}
