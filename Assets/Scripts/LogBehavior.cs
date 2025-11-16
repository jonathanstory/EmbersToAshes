using System.Collections;
using UnityEngine;

public class LogBehavior : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterMovementSimple player = collision.gameObject.GetComponent<CharacterMovementSimple>();

            if(player != null)
                if (player.isDashing == false)
                    StartCoroutine(pickup());
        }
    }

    private IEnumerator pickup()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
