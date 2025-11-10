using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public RawImage splashImage;
    public VideoPlayer videoPlayer;
    public float fadeDuration = 1f;

    void Start()
    {
        splashImage.color = new Color(1, 1, 1, 0);
        videoPlayer.Play();
        StartCoroutine(FadeInOut());
    }

    System.Collections.IEnumerator FadeInOut()
    {
        // Fade In
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            splashImage.color = new Color(1, 1, 1, Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }

        // Fade Out
        t = -3;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            splashImage.color = new Color(1, 1, 1, 1 - Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }

        // Hide splash screen
        splashImage.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenuScreen");
    }
}

