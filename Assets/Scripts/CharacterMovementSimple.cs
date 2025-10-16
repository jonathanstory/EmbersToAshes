using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementSimple : MonoBehaviour
{
    public int maxPlayerHealth, currentPlayerHealth;
    public float maxLightRange;
    public float rotationSpeed;
    public bool canDash = false;

    public Light playerLight;
    public Light playerLightOrigin;
    public GameObject homeBase;

    public float moveSpeed = 5;
    public float invincibilityTime = 2.0f;

    public LightBarScript lightBar;
    PlayerControls controls;
    private Vector3 movement;

    private Quaternion rotation;
    private bool isPaused;
    private bool isInvincible;

    private Rigidbody rb;

    private PlayerInput playerInput;
    private InputAction moving;

    public AudioSource audioSource;
    public AudioClip[] clips;
    private int stepCount = 0;
    private float stepTimer = 0f;
    private float stepDelay = .4f;

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
        rb = GetComponent<Rigidbody>();
        lightBar.SetMaxHealth(maxPlayerHealth);
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moving = playerInput.actions["Move"];
    }

    private void Update()
    {
        playerLight.spotAngle = maxLightRange * ((float)currentPlayerHealth / (float)maxPlayerHealth);
        playerLightOrigin.intensity = maxLightRange *((float)currentPlayerHealth / (float)maxPlayerHealth);
        lightBar.SetHealth(currentPlayerHealth);
        if (currentPlayerHealth <= 0)
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

            if (moving.IsPressed())
            {
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotation, Time.fixedDeltaTime * rotationSpeed));
                stepTimer += Time.deltaTime;
                if(stepTimer > stepDelay)
                {
                    PlayFootstep();
                    stepTimer = 0f;
                }
            }
        }
    }

    private void PlayFootstep()
    {
        if (stepCount == 0)
        {
            stepCount = 1;
            audioSource.PlayOneShot(clips[0]);
        }
        else
        {
            stepCount = 0;
            audioSource.PlayOneShot(clips[1]);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y), Vector3.up);
    }

    public void OnSprint()
    {
        if (canDash)
        {
            canDash = false;
            StartCoroutine(DashTimer());
        }
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
            if(GameManager.Instance.localInventoryCurrent < GameManager.Instance.localInventoryMax)
            GameManager.Instance.localWood += 1;
        }
        if(other.gameObject.CompareTag("Stone"))
        {
            if(GameManager.Instance.localInventoryCurrent < GameManager.Instance.localInventoryMax)
            GameManager.Instance.localStone += 1;
        }

        if (other.gameObject.CompareTag("Base"))
        {
            GameManager.Instance.InventoryConvert(); 
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

    private IEnumerator DashTimer()
    {
        moveSpeed *= 2;
        yield return new WaitForSeconds(0.3f);
        moveSpeed /= 2;
        yield return new WaitForSeconds(invincibilityTime);
        canDash = true;
    }
}
