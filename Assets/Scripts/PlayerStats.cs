using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int baseHealth = 3;
    public float baseDashCooldown = 2.0f;
    public int baseInventoryMax = 5;
    public float baseFireBurnRate = 25f;
    public float baseFireAddValue = 250f;
    public int baseStartingWood = 0;
    public int baseStartingStone = 0;
    public float baseStealth = 1.2f;
    public int embers;

    private void Start()
    {
        //if(!PlayerPrefs.HasKey("MaxHP"))
        //{
            PlayerPrefs.SetInt("MaxHP", baseHealth);
            PlayerPrefs.SetFloat("DashCooldown", baseDashCooldown);
            PlayerPrefs.SetInt("InventoryMax", baseInventoryMax);
            PlayerPrefs.SetFloat("FireBurnRate", baseFireBurnRate);
            PlayerPrefs.SetFloat("FireAddValue", baseFireAddValue);
            PlayerPrefs.SetInt("StartingWood", baseStartingWood);
            PlayerPrefs.SetInt("StartingStone", baseStartingStone);
            PlayerPrefs.SetFloat("Stealth", baseStealth);
            PlayerPrefs.SetInt("Embers", 0);
            PlayerPrefs.Save();

            Debug.Log("Keys Created");
        //}
    }


    public void ApplyHealthUpgrade(int health)
    {
        PlayerPrefs.SetInt("MaxHP", baseHealth + health);
        PlayerPrefs.Save();
    }

    public void ApplyDashCooldownUpgrade(float speed)
    {
        PlayerPrefs.SetFloat("DashCooldown", baseDashCooldown - speed);
        PlayerPrefs.Save();
    }

    public void ApplyInventoryUpgrade(int value)
    {
        PlayerPrefs.SetInt("MaxInventory", baseInventoryMax + value);
        PlayerPrefs.Save();
    }

    public void ApplyFireBurnRateUpgrade(float value)
    {
        PlayerPrefs.SetFloat("FireBurnRate", baseFireBurnRate + value);
        PlayerPrefs.Save();
    }

    public void ApplyFireAddValueUpgrade(float value)
    {
        PlayerPrefs.SetFloat("FireAddValue", baseFireAddValue + value);
        PlayerPrefs.Save();
    }

    public void ApplyStartingWoodUpgrade(int value)
    {
        PlayerPrefs.SetInt("StartingWood", baseStartingWood + value);
        PlayerPrefs.Save();
    }

    public void ApplyStartingStoneUpgrade(int value)
    {
        PlayerPrefs.SetInt("StartingStone", baseStartingStone + value);
        PlayerPrefs.Save();
    }

    public void ApplyStealthUpgrade(float value)
    {
        PlayerPrefs.SetFloat("Stealth", baseStealth - value);
        PlayerPrefs.Save();
    }

    public void AddEmbers(int value)
    {
        PlayerPrefs.SetInt("Embers", PlayerPrefs.GetInt("Embers") + value);
        PlayerPrefs.Save();
    }
}
