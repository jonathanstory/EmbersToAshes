using UnityEngine;

public class HealPlayer : MonoBehaviour
{

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && player.GetComponent<CharacterMovementSimple>().currentPlayerHealth != player.GetComponent<CharacterMovementSimple>().maxPlayerHealth)
        {
            if (GameManager.Instance.localWood >= 1)
            {
                GameManager.Instance.localWood -= 1;
                player.GetComponent<CharacterMovementSimple>().currentPlayerHealth += 1;
            }
        }
    }
}
