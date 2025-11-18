using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

public class upgradePipsBar : MonoBehaviour
{
    public GameObject PipPrefab;
    public float pipUpgrades, maxPipUpgrades;
    List<pipUpdater> pips = new List<pipUpdater>();

    private void Start()
    {
        DrawPips();
    }

    public void CreateEmptyPip()
    {
        GameObject newPip = Instantiate(PipPrefab);
        newPip.transform.SetParent(transform);

        pipUpdater pipComponent = newPip.GetComponent<pipUpdater>();
        pipComponent.setPipImage(pipStatus.unfilled);
        pips.Add(pipComponent);
    }
    public void ClearPips()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        pips = new List<pipUpdater>();

    }

    //Lexx Note: Hey there, this is the function you need to call in that specific place when the upgrade is made, and you need to hold the upgrades value for each of these lists, or something like that.
    public void DrawPips()
    {
        ClearPips();

        //determine how many pips to make total, based off max upgrades
        int pipsToMake = (int)(maxPipUpgrades);
        for (int i = 0; i < pipsToMake; i++)
        {
            CreateEmptyPip();
            
        }
        for (int i = 0; i <pips.Count; i++)
        {
            int pipStatusRemainder = (int)Mathf.Clamp(pipUpgrades - (i), 0, 1);
            pips[i].setPipImage((pipStatus)pipStatusRemainder);
            Debug.Log("I was here");
        }
    }
}
