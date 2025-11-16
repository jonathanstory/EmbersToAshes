using UnityEngine;

public class FireBehavior : MonoBehaviour
{

    public float currentFireLife;
    public float maxFireLife;
    public float rateOfDecay;
    public float maxRange;

    public float originIntensity;

    public Light fireLight;
    public Light shadowLight;

    public LightBarScript lightBar; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightBar = GameObject.FindGameObjectWithTag("LightBar").GetComponent<LightBarScript>();

        maxFireLife = 1000;
        lightBar.SetMaxHealth(maxFireLife);
        currentFireLife = maxFireLife;
        fireLight.spotAngle = maxRange;

        rateOfDecay = PlayerPrefs.GetFloat("FireBurnRate");
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(currentFireLife,0,maxFireLife);

        currentFireLife -= Time.deltaTime * rateOfDecay;
        lightBar.SetHealth(currentFireLife);

        fireLight.spotAngle = maxRange * (currentFireLife/maxFireLife);

        shadowLight.intensity = originIntensity * (currentFireLife / maxFireLife);

        if(currentFireLife <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    public void FuelFire()
    {
        if(GameManager.Instance.localWood > 0)
        {
            currentFireLife += PlayerPrefs.GetFloat("FireAddValue");
            GameManager.Instance.localWood -= 1;
        }
    }
}
