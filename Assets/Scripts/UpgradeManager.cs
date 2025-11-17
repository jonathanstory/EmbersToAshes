using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject[] buttons;

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
                }
                if (i == 1)
                {
                    text.SetText("Reduces cooldown of player dash");
                }
                if (i == 2)
                {
                    text.SetText("Reduces player detection");
                }
                if (i == 3)
                {
                    text.SetText("Increases player inventory");
                }
                if (i == 4)
                {
                    text.SetText("Reduces the rate at which your campfire depletes");
                }
                if (i == 5)
                {
                    text.SetText("Increases value of fuel added to fire");
                }
                if (i == 6)
                {
                    text.SetText("Gives player starting wood");
                }
                if (i == 7)
                {
                    text.SetText("Gives player starting stone");
                }
            }
            else
            {
                buttons[i].SetActive(false);
            }
        }
    }

    public void SelectDash()
    {

    }

    public void SelectStealth()
    {

    }

    public void SelectInventory()
    {

    }

    public void SelectBurnRate()
    {

    }

    public void SelectFuelValue()
    {

    }

    public void SelectWoodSupply()
    {

    }

    public void SelectStoneSupply()
    {

    }

    public void UpgradeHealth(int level)
    {
        int[] values = {1, 2, 3};
        PlayerStats.Instance.ApplyHealthUpgrade(values[level]);
    }

    public void UpgradeDashCooldown(int level)
    {
        float[] values = {0.2f, 0.3f, 0.4f};
        PlayerStats.Instance.ApplyDashCooldownUpgrade(values[level]);
    }

    public void UpgradeInventory()
    {
        PlayerStats.Instance.ApplyInventoryUpgrade(2);
    }

    public void UpgradeFireBurnRate(int level)
    {
        float[] values = {2.5f, 3.5f, 5f};
        PlayerStats.Instance.ApplyFireBurnRateUpgrade(values[level]);
    }

    public void UpgradeFireAddValue(int level)
    {
        float[] values = {10f, 20f, 30f};
        PlayerStats.Instance.ApplyFireAddValueUpgrade(values[level]);
    }

    public void UpgradeStartingWood(int level)
    {
        int[] values = { 1, 2, 3 };
        PlayerStats.Instance.ApplyStartingWoodUpgrade(values[level]);
    }

    public void UpgradeStartingStone(int level)
    {
        int[] values = { 1, 2, 3 };
        PlayerStats.Instance.ApplyStartingStoneUpgrade(values[level]);
    }

    public void UpgradeStealth(int level)
    {
        float[] values = { 0.1f, 0.15f, 0.2f };
        PlayerStats.Instance.ApplyStealthUpgrade(values[level]);
    }
}
