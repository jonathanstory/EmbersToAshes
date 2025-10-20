using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    public string sceneName;
    private float fadeTimer = 0;
    private float fadeDuration = 4f;
    public GameObject fadeObject;
    public Image fadeImage;
    public GameObject survivalTip;
    public static GameManager Instance;

    private string thisScene;

    public void NextScene()
    {
        thisScene = SceneManager.GetActiveScene().name;

        if (sceneName == "gameplayScene" && thisScene != "gameOverScreen")
        {
            StartCoroutine(FadeScreen());
        }
        else
        {
            GameManager.Instance.ResetGame();
            SceneManager.LoadScene(sceneName);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeScreen()
    {
        fadeObject.SetActive(true);

        Color startColor = fadeImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while(fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime / 1000;
            fadeImage.color = Color.Lerp(startColor, targetColor, fadeTimer / fadeDuration);
        }

        fadeTimer = 0;
        survivalTip.SetActive(true);

        while(fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime / 1000;
            fadeImage.color = Color.Lerp(targetColor, startColor, fadeTimer / fadeDuration);
        }

        fadeTimer = 0;

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(sceneName);
    }
}
