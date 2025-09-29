using UnityEngine;

public class FireBehavior : MonoBehaviour
{

    public float currentFireLife;
    public float maxFireLife;
    public float rateOfDecay;
    public float maxRange;

    public Light fireLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxFireLife = 10000;
        currentFireLife = maxFireLife;
        maxRange = 50;
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Clamp(currentFireLife,0,maxFireLife);

        currentFireLife -= Time.deltaTime * rateOfDecay;

        fireLight.intensity = currentFireLife;
        fireLight.range = maxRange * (currentFireLife / maxFireLife);

        if(currentFireLife <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
