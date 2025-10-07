using UnityEngine;

public class Campfire : MonoBehaviour
{
    public float stickGrowth = 0.2f;

    public void AddSticks(int sticks)
    {
        transform.localScale += new Vector3(stickGrowth, stickGrowth, stickGrowth) * sticks;
    }
}
