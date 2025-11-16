using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CharacterMovementSimple : MonoBehaviour
{
    public int maxPlayerHealth, currentPlayerHealth;
    public float maxLightRange;
    public float rotationSpeed;
    public bool canDash = false;
    public float dashCooldown;
    private Animator animator;

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
    public bool isDashing;

    private Rigidbody rb;

    private PlayerInput playerInput;
    private InputAction moving;

    public AudioSource audioSource;
    public AudioClip[] clips;
    private int stepCount = 0;
    private float stepTimer = 0f;
    private float stepDelay = .4f;

    public TextMeshProUGUI currentLocalWood;
    public TextMeshProUGUI currentLocalStone;

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
        currentLocalWood = GameObject.FindGameObjectWithTag("LocalW").GetComponent<TextMeshProUGUI>();
        currentLocalStone = GameObject.FindGameObjectWithTag("LocalS").GetComponent<TextMeshProUGUI>();

        maxPlayerHealth = PlayerPrefs.GetInt("MaxHP");

        currentPlayerHealth = maxPlayerHealth;
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moving = playerInput.actions["Move"];
    }

    private void Update()
    {
        playerLight.spotAngle = maxLightRange * ((float)currentPlayerHealth / (float)maxPlayerHealth);
        playerLightOrigin.intensity = maxLightRange * ((float)currentPlayerHealth / (float)maxPlayerHealth);

        lightBar.SetHealth(currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }

        if (movement == Vector3.zero)
        {
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }

        isDashing = animator.GetBool("IsDashing");

        currentLocalWood.SetText("x " + GameManager.Instance.localWood + "/" + GameManager.Instance.localInventoryMax);
        currentLocalStone.SetText("x " + GameManager.Instance.localStone + "/" + GameManager.Instance.localInventoryMax);

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
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Rake"))
        {
            if(!isInvincible)
                currentPlayerHealth -= 1;
        }

        if(other.gameObject.CompareTag("Wood") && !isDashing)
        {
            animator.SetTrigger("Pickup");
            StartCoroutine(PickUpTime());

            if (GameManager.Instance.localInventoryCurrent < GameManager.Instance.localInventoryMax)
                GameManager.Instance.localWood += 1;
        }
        if(other.gameObject.CompareTag("Stone") && !isDashing)
        {
            animator.SetTrigger("Pickup");
            StartCoroutine(PickUpTime());

            if (GameManager.Instance.localInventoryCurrent < GameManager.Instance.localInventoryMax)
                GameManager.Instance.localStone += 1;
        }

        if (other.gameObject.CompareTag("Base"))
        {
            GameManager.Instance.InventoryConvert();
            GameManager.Instance.localStone = 0;
            GameManager.Instance.localWood = 0;
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
        animator.SetBool("IsDashing", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("IsDashing", false);
        moveSpeed /= 2;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (!isInvincible)
                currentPlayerHealth -= 1;
        }
    }

    private IEnumerator PickUpTime()
    {
        playerInput.enabled = false;
        yield return new WaitForSeconds(0.1f);
        movement = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        playerInput.enabled = true;
    }
}
