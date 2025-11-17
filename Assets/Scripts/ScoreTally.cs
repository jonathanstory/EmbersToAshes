using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreTally : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public TextMeshProUGUI score;
    public TextMeshProUGUI embers;


    private void Awake()
    {
        score.SetText("Time Survived: \n" + Mathf.Round(GameManager.Instance.timeSurvived));
        embers.SetText("Embers earned: \n" + Mathf.Round(GameManager.Instance.timeSurvived / 2));
    }
}
