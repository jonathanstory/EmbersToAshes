using UnityEngine;
using UnityEngine.UI;

public class ResourceText : MonoBehaviour
{
    [SerializeField] Text resourceText;

    int resource = 0;
private void Awake ()
    {
        UpdateHUD();
    }
    public int Resource
    {
        get
        {
            return resource;
        }
        set
        {
            resource = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD ()
    {
        resourceText.text = "x " + resource.ToString();
    }
 }
