using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementSimple : MonoBehaviour
{

    public float moveSpeed = 5;
    PlayerControls controls;
    private Vector3 movement;

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
        
    }

    private void LateUpdate()
    {
        if (movement != null)
        {
            transform.position += new Vector3(movement.x * moveSpeed * Time.deltaTime, 0, movement.y * moveSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();

    }

    
}
