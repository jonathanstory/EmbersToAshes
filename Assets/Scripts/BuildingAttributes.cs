using UnityEngine;

public class BuildingAttributes : MonoBehaviour
{
    public int woodCost;
    public int stoneCost;

    public bool affectsResource;
    public bool affectsFireRate;

    public int effectPower;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        if(affectsFireRate)
        {
            GameObject playerBase = GameObject.FindGameObjectWithTag("Base");

            playerBase.GetComponent<FireBehavior>().rateOfDecay -= effectPower;
        }

        if(affectsResource)
        {
            GameManager.Instance.globalInventoryMax += effectPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
