using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats stats;

    public void UpgradeHealth(int level)
    {
        float[] values = {0.05f, 0.10f, 0.15f};
        stats.ApplyHealthUpgrade(values[level]);
    }

    public void UpgradeMoveSpeed(int level)
    {
        float[] values = {0.05f, 0.07f, 0.10f};
        stats.ApplyMoveSpeedUpgrade(values[level]);
    }

    public void UpgradeInventory()
    {
        stats.ApplyInventoryUpgrade(4);
    }

    public void UpgradeFireBurnRate(int level)
    {
        float[] values = {0.03f, 0.05f, 0.08f};
        stats.ApplyFireBurnRateUpgrade(values[level]);
    }

    public void UpgradeFireAddValue(int level)
    {
        float[] values = {0.03f, 0.05f, 0.08f};
        stats.ApplyFireAddValueUpgrade(values[level]);
    }

    public void UpgradeStartingResources()
    {
        stats.ApplyStartingResources(2, 2);
    }

    public void UpgradeStealth()
    {
        stats.ApplyStealthUpgrade(0.03f);
    }
}
