using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentWood;
    public float timeSurvived;

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

            currentWood = 0;
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
}
