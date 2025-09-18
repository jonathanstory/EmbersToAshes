using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementSimple : MonoBehaviour
{

    public float moveSpeed = 5;
    PlayerControls controls;

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
            // Tell the "gameplay" action map that we want to get told about
            // when actions get triggered.
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {

    }

    
}
