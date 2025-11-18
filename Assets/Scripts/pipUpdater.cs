using UnityEngine;
using UnityEngine.UI;

public class pipUpdater : MonoBehaviour
{
    public Sprite unfilledPip, filledPip;
    Image pipImage;

    private void Awake()
    {
        pipImage = GetComponent<Image>();
    }

    public void setPipImage(pipStatus status)
    {
        switch (status)
        {
            case pipStatus.unfilled:
                pipImage.sprite = unfilledPip;
                break;
            case pipStatus.filled:
                pipImage.sprite = filledPip;
                break;
        }

    }
}
public enum pipStatus
{
    unfilled = 0,
    filled = 1
}