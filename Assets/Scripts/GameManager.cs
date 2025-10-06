using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

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

            timeSurvived = 0;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "gameplayScene")
        {
            timeSurvived += Time.deltaTime * 1;

            if (timeSurvived > 60)
            {
                GameWin();
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("gameOverScreen");
    }

    public void GameWin()
    {
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
            
}
