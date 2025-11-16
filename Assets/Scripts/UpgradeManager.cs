using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats stats;

    public void UpgradeHealth(int level)
    {
        int[] values = {1, 2, 3};
        stats.ApplyHealthUpgrade(values[level]);
    }

    public void UpgradeDashCooldown(int level)
    {
        float[] values = {0.2f, 0.3f, 0.4f};
        stats.ApplyDashCooldownUpgrade(values[level]);
    }

    public void UpgradeInventory()
    {
        stats.ApplyInventoryUpgrade(2);
    }

    public void UpgradeFireBurnRate(int level)
    {
        float[] values = {2.5f, 3.5f, 5f};
        stats.ApplyFireBurnRateUpgrade(values[level]);
    }

    public void UpgradeFireAddValue(int level)
    {
        float[] values = {10f, 20f, 30f};
        stats.ApplyFireAddValueUpgrade(values[level]);
    }

    public void UpgradeStartingWood(int level)
    {
        int[] values = { 1, 2, 3 };
        stats.ApplyStartingWoodUpgrade(values[level]);
    }

    public void UpgradeStartingStone(int level)
    {
        int[] values = { 1, 2, 3 };
        stats.ApplyStartingStoneUpgrade(values[level]);
    }

    public void UpgradeStealth(int level)
    {
        float[] values = { 0.1f, 0.15f, 0.2f };
        stats.ApplyStealthUpgrade(values[level]);
    }
}
