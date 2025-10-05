using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementSimple : MonoBehaviour
{
    public int maxPlayerHealth, currentPlayerHealth;
    public float maxLightIntensity;
    public float maxLightRange;
    public float rotationSpeed;

    public Light playerLight;
    public GameObject homeBase;

    public float moveSpeed = 5;
    public float invincibilityTime = 2.0f;
    PlayerControls controls;
    private Vector3 movement;

    private Quaternion rotation;
    private bool isPaused;
    private bool isInvincible;


    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
            // Tell the "gameplay" action map that we want to get told about
            // when actions get triggered.
        }
        controls.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
        homeBase = GameObject.Find("campfire");
    }

    private void Update()
    {
        playerLight.intensity = maxLightIntensity * ((float)currentPlayerHealth / (float)maxPlayerHealth);
        playerLight.range = maxLightRange * ((float)currentPlayerHealth / (float)maxPlayerHealth);

        if(currentPlayerHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void LateUpdate()
    {
        if (movement != null)
        {
            Vector3 movementVector = new Vector3(movement.x * moveSpeed * Time.deltaTime, 0, movement.y * moveSpeed * Time.deltaTime);

            transform.position += movementVector;

            Quaternion rotation = Quaternion.LookRotation(movementVector, Vector3.up);

            transform.rotation = Quaternion.Slerp(this.rotation, rotation, rotationSpeed);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnPause()
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }

    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(!isInvincible)
                currentPlayerHealth -= 1;
        }

        if(other.gameObject.CompareTag("Wood"))
        {
            GameManager.Instance.currentWood += 1;
        }

        if (other.gameObject.CompareTag("Base"))
        {
            if (GameManager.Instance.currentWood > 0)
            {
                GameManager.Instance.currentWood -= 1;
                homeBase.GetComponent<FireBehavior>().currentFireLife += 5000;
            }
        }
    }

    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            isInvincible = true;
            StartCoroutine(InvincibilityTimer());
        }
    }

    private IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

}
