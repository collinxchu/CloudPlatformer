using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	// Player movement variables.
	public Vector3 newSpeed;
	int jumpCount;
	bool updateMovement = false;
	
	// Input system reference.
	PlayerInput playerInput;
	
	[Header("References")]
	public Rigidbody collider;
	public Transform cameraAnchor;
	public Camera camera;
	public Text debug;
	
	[Header("Variables")]
	public float charSpeed = 10;
	public float charJumpHeight = 6;
	public int charJumps = 1;
	
	float distToGround = 1f;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		// Enable (default) action set.
        playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Enable();
        playerInput.currentActionMap?.Enable();
    }
	
	public bool IsGrounded()
	{
		if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.01f))
		{
			//Debug.Log("Grounded");
			return true;
		}
		else
		{
			//Debug.Log("Not grounded");
			return false;
		}
	}
	
    // This is triggered when the "Move" input action is activated.
	// Look into "Unity input actions" if you want to know more.
	public void OnMove(InputValue value){
		var charMove = value.Get<Vector2>();
		if (charMove == Vector2.zero) {
			// If our 'move' value is neutral input, stop moving.
			updateMovement = false;
		}
		else
		{
			// Otherwise, set our target direction and movement, 
			// then toggle movement on.
			newSpeed = Vector3.up * collider.linearVelocity.y;
			newSpeed.x = charMove.x * charSpeed;
			newSpeed.z = charMove.y * charSpeed;
			updateMovement = true;
		}
    }
	
	// Same as above, but with "Jump".
    public void OnJump(){
		
        if(playerInput.actions["Jump"].WasPressedThisFrame())
		{
			if ((IsGrounded()) || (jumpCount > 0))
			{
				// I separated this into its own function,
				// Just in case we want to call a jump
				// from some other context.
				Jump();
			}
		}
    }
	
	void Jump() {
		var jump = new Vector3(0,charJumpHeight,0);
		collider.AddForce(jump, ForceMode.VelocityChange);
		--jumpCount;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate() {
		if (IsGrounded())
		{
			jumpCount = charJumps;
		}
		if (updateMovement) {
			// If we're allowed to move, move in the direction indicated.
			// You can also set newSpeed manually some other way.
			// TODO: Integrate animation states.
			collider.linearVelocity = newSpeed;
		}
	}
}