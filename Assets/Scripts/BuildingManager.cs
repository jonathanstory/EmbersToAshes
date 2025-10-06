using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject buildingUI;
    public GameObject playerBase;

    private Quaternion baseDir;
    private bool UIActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIActive = false;
    }

    private void Awake()
    {
        Vector3 direction = playerBase.transform.position - transform.position;
        baseDir = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateUI()
    {
        if (!UIActive)
        {
            buildingUI.gameObject.SetActive(true);
            UIActive = true;
        }
        else
        {
            buildingUI.gameObject.SetActive(false);
            UIActive = false;
        }
    }

    public void InstantiateBuilding(int buildingIndex, int woodCost)
    {
        if (GameManager.Instance.globalWood > woodCost)
        { 
            Instantiate(buildings[buildingIndex], this.transform.position, baseDir);
            Destroy(gameObject);
            GameManager.Instance.globalWood -= woodCost;
        }
    }

}
 