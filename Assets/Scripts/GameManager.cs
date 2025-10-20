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

        Debug.Log("Global Wood: " + globalWood);
        Debug.Log("Global Stone: " + globalStone);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("gameOverScreen");
    }

    public void GameWin()
    {
        ResetGame();
        SceneManager.LoadScene("winScreen");
    }

    public void InventoryConvert()
    {
        if (globalInventoryCurrent < globalInventoryMax)
        {
            globalWood += localWood;

            globalStone += localStone;
        }
    }      
    
    public void ResetGame()
    {
        localInventoryMax = 5;
        localInventoryCurrent = 0;
        globalInventoryMax = 5;
        globalInventoryCurrent = 0;
        localWood = 0;
        localStone = 0;
        globalWood = 0;
        globalStone = 0;
        timeSurvived = 0;
    }
}
