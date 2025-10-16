using UnityEngine;
using UnityEngine.UI;

public class LightBarScript : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health; //May need to remove this in case you want the fire to not get brought to max upon upgrading.
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
