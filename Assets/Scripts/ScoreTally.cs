using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTally : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public TextMeshProUGUI score;


    private void Awake()
    {
        score.SetText("Time Survived: \n" + Mathf.Round(GameManager.Instance.timeSurvived));
    }
}
