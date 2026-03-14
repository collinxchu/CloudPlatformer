using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	PlayerInput playerInput;
	int jumpCount;
	
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
			Debug.Log("Grounded");
			return true;
		}
		else
		{
			Debug.Log("Not grounded");
			return false;
		}
	}
	
	// Not called if the 'move' state hasn't changed. TODO: Fix.
    public void OnMove(InputValue value){
        var charMove = value.Get<Vector2>();
        Vector3 newSpeed = Vector3.up * collider.linearVelocity.y;
        newSpeed.x = charMove.x * charSpeed;
        newSpeed.z = charMove.y * charSpeed;
        collider.linearVelocity = newSpeed;
    }
	
    public void OnJump(){
		
        if(playerInput.actions["Jump"].WasPressedThisFrame())
			if ((IsGrounded()) || (jumpCount > 0))
			{
				Jump();
			}
    }
	
	void Jump() {
		collider.linearVelocity = new Vector3(collider.linearVelocity.x, charJumpHeight, collider.linearVelocity.z);
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
	}
}