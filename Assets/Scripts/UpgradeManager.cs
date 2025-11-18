using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI[] cost;
    public TextMeshProUGUI embers;
    public GameObject[] buttons;
    public GameObject[] pips;
    private int baseCost = 50;

    public void SelectPurchase(int index)
    {
        for (int i = 0; i < 8; i++)
        {
            if(i == index)
            {
                buttons[i].SetActive(true);
                if(i == 0)
                {
                    text.SetText("Adds additional health to the player");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("MaxHPCount") + 1)).ToString());
                }
                if (i == 1)
                {
                    text.SetText("Reduces cooldown of player dash");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("DashCooldownCount") + 1)).ToString());
                }
                if (i == 2)
                {
                    text.SetText("Reduces player detection");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("StealthCount") + 1)).ToString());
                }
                if (i == 3)
                {
                    text.SetText("Increases player inventory");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("InventoryCount") + 1)).ToString());
                }
                if (i == 4)
                {
                    text.SetText("Reduces the rate at which your campfire depletes");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("FireBurnRateCount") + 1)).ToString());
                }
                if (i == 5)
                {
                    text.SetText("Increases value of fuel added to fire");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("FireAddValueCount") + 1)).ToString());
                }
                if (i == 6)
                {
                    text.SetText("Gives player starting wood");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("StartingWoodCount") + 1)).ToString());
                }
                if (i == 7)
                {
                    text.SetText("Gives player starting stone");
                    cost[i].SetText((baseCost * (PlayerPrefs.GetInt("StartingStoneCount") + 1)).ToString());
                }
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public bool getCost()
    {
        return true;
    }

    public void UpgradeHealth()
    {
        if (PlayerPrefs.GetInt("MaxHPCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("MaxHPCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("MaxHPCount") + 1)));
                int[] values = { 1, 2, 3 };
                PlayerStats.Instance.ApplyHealthUpgrade(values[PlayerPrefs.GetInt("MaxHPCount")]);
                cost[0].SetText((baseCost * (PlayerPrefs.GetInt("MaxHPCount") + 1)).ToString());
                pips[0].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeDashCooldown()
    {
        if (PlayerPrefs.GetInt("DashCooldownCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("DashCooldownCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("DashCooldownCount") + 1)));
                float[] values = { 0.2f, 0.3f, 0.4f };
                PlayerStats.Instance.ApplyDashCooldownUpgrade(values[PlayerPrefs.GetInt("DashCooldownCount")]);
                cost[1].SetText((baseCost * (PlayerPrefs.GetInt("DashCooldownCount") + 1)).ToString());
                pips[1].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeInventory()
    {
        if (PlayerPrefs.GetInt("InventoryCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("InventoryCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("InventoryCount") + 1)));
                int[] values = { 1, 2, 3 };
                PlayerStats.Instance.ApplyInventoryUpgrade(values[PlayerPrefs.GetInt("InventoryCount")]);
                cost[3].SetText((baseCost * (PlayerPrefs.GetInt("InventoryCount") + 1)).ToString());
                pips[3].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeFireBurnRate()
    {
        if (PlayerPrefs.GetInt("FireBurnRateCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("FireBurnRateCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("FireBurnRateCount") + 1)));
                float[] values = { 2.5f, 3.5f, 5f };
                PlayerStats.Instance.ApplyFireBurnRateUpgrade(values[PlayerPrefs.GetInt("FireBurnRateCount")]);
                cost[4].SetText((baseCost * (PlayerPrefs.GetInt("FireBurnRateCount") + 1)).ToString());
                pips[4].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeFireAddValue()
    {
        if (PlayerPrefs.GetInt("FireAddValueCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("FireAddValueCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("FireAddValueCount") + 1)));
                float[] values = { 10f, 20f, 30f };
                PlayerStats.Instance.ApplyFireAddValueUpgrade(values[PlayerPrefs.GetInt("FireAddValueCount")]);
                cost[5].SetText((baseCost * (PlayerPrefs.GetInt("FireAddValueCount") + 1)).ToString());
                pips[5].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeStartingWood()
    {
        if (PlayerPrefs.GetInt("StartingWoodCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("StartingWoodCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("StartingWoodCount") + 1)));
                int[] values = { 1, 2, 3 };
                PlayerStats.Instance.ApplyStartingWoodUpgrade(values[PlayerPrefs.GetInt("StartingWoodCount")]);
                cost[6].SetText((baseCost * (PlayerPrefs.GetInt("StartingWoodCount") + 1)).ToString());
                pips[6].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    public void UpgradeStartingStone()
    {
        if (PlayerPrefs.GetInt("StartingStoneCount") < 3)
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("StartingStoneCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("StartingStoneCount") + 1)));
                {
                    int[] values = { 1, 2, 3 };
                    PlayerStats.Instance.ApplyStartingStoneUpgrade(values[PlayerPrefs.GetInt("StartingStoneCount")]);
                    cost[7].SetText((baseCost * (PlayerPrefs.GetInt("StartingStoneCount") + 1)).ToString());
                    pips[7].GetComponent<upgradePipsBar>().DrawPips();
                    PlayerPrefs.Save();
                }
        }
    }

    public void UpgradeStealth()
    {
        if (PlayerPrefs.GetInt("StealthCount") < 3)
        {
            if (PlayerPrefs.GetInt("Embers") >= (baseCost * (PlayerPrefs.GetInt("StealthCount") + 1)))
            {
                PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") - (baseCost * (PlayerPrefs.GetInt("StealthCount") + 1)));
                float[] values = { 0.1f, 0.15f, 0.2f };
                PlayerStats.Instance.ApplyStealthUpgrade(values[PlayerPrefs.GetInt("StealthCount")]);
                cost[2].SetText((baseCost * (PlayerPrefs.GetInt("StealthCount") + 1)).ToString());
                pips[2].GetComponent<upgradePipsBar>().DrawPips();
                PlayerPrefs.Save();
            }
        }
    }

    private void Update()
    {
        embers.SetText(PlayerPrefs.GetInt("Embers").ToString());
    }
}
