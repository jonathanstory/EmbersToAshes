using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementSimple : MonoBehaviour
{

    public float moveSpeed = 5;
    PlayerControls controls;
    private Vector3 movement;

    private Quaternion rotation;
    private bool isPaused;

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
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (movement != null)
        {
            transform.position += new Vector3(movement.x * moveSpeed * Time.deltaTime, 0, movement.y * moveSpeed * Time.deltaTime);

            //Quaternion rotation = Quaternion.LookRotation(movement, Vector3.up);

            //transform.rotation = new Quaternion(0,rotation.y,0,0);
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

    
}
