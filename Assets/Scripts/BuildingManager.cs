using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildings;
    private GameObject[] buildingSpots;
    public GameObject buildingUI;
    public GameObject playerBase;

    private Quaternion baseDir;
    private bool UIActive;

    public AudioClip[] clips;

    public AudioSource audioSource;

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

    public void InstantiateBuilding(int buildingIndex)
    {
        if (GameManager.Instance.globalWood >= buildings[buildingIndex].GetComponent<BuildingAttributes>().woodCost)
        { 
            Instantiate(buildings[buildingIndex], this.transform.position, baseDir);
            GameManager.Instance.globalWood -= buildings[buildingIndex].GetComponent<BuildingAttributes>().woodCost;

            StartCoroutine(PlayClips());
        }
    }

    private IEnumerator PlayClips()
    {
        foreach (AudioClip clip in clips)
        {
            audioSource.clip = clip;
            audioSource.Play();

            yield return new WaitForSeconds(clip.length);
        }

        if (transform.parent != null)
            Destroy(transform.parent.gameObject);
        else
            Destroy(gameObject);
    }
}
 