using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timeSurvived;

    public int localInventoryMax;
    public int localWood;
    public int localStone;
    public int localInventoryCurrent;
    public int globalInventoryMax;
    public int globalStone;
    public int globalWood;
    public int globalInventoryCurrent;
    public static GameManager Instance { get; private set; }

    public PlayerStats emberCount;

    public TextMeshProUGUI currentGlobalWood;
    public TextMeshProUGUI currentGlobalStone;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes

            ResetGame();
            SetPlayerStats();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "gameplayScene")
        {
            timeSurvived += Time.deltaTime * 1;
            currentGlobalWood = GameObject.FindGameObjectWithTag("WoodUI").GetComponent<TextMeshProUGUI>();
            currentGlobalStone = GameObject.FindGameObjectWithTag("StoneUI").GetComponent<TextMeshProUGUI>();

            currentGlobalWood.SetText("x " + globalWood.ToString() + "/" + globalInventoryMax);
            currentGlobalStone.SetText("x " + globalStone.ToString() + "/" + globalInventoryMax);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("gameOverScreen");
        emberCount.AddEmbers((int)Mathf.Round((timeSurvived / 2)));
    }

    public void InventoryConvert()
    {
        if (globalInventoryCurrent < globalInventoryMax)
        {
            globalWood += localWood;

            globalStone += localStone;

            globalWood = Mathf.Clamp(globalWood, 0, globalInventoryMax);
            globalStone = Mathf.Clamp(globalStone, 0, globalInventoryMax);
        }
    }      
    
    public void ResetGame()
    {
        localWood = PlayerPrefs.GetInt("StartingWood");
        localStone = PlayerPrefs.GetInt("StartingStone");
        globalWood = 0;
        globalStone = 0;
        timeSurvived = 0;
    }

    public void SetPlayerStats()
    {
        localInventoryMax = PlayerPrefs.GetInt("InventoryMax");
        localInventoryCurrent = 0;
        globalInventoryMax = PlayerPrefs.GetInt("InventoryMax");
        globalInventoryCurrent = 0;
    }
}
