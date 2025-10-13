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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxFireLife = 1000;
        currentFireLife = maxFireLife;
        fireLight.spotAngle = maxRange;
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(currentFireLife,0,maxFireLife);

        currentFireLife -= Time.deltaTime * rateOfDecay;

        fireLight.spotAngle = maxRange * (currentFireLife/maxFireLife);

        shadowLight.intensity = originIntensity * (currentFireLife / maxFireLife);

        if(currentFireLife <= 0)
        {
            GameManager.Instance.GameOver();
        }

        Debug.Log(currentFireLife);
    }

    public void FuelFire()
    {
        if(GameManager.Instance.localWood > 0)
        {
            currentFireLife += 250;
            GameManager.Instance.localWood -= 1;
        }
    }
}
