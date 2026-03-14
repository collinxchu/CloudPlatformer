using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  PlayerInput playerInput;

  [Header("References")]
  public Rigidbody collider;
  public Transform cameraAnchor;
  public Camera camera;

  [Header("Variables")]
  public float charSpeed;
  public float charJumpHeight;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Enable();
        playerInput.currentActionMap?.Enable();
    }
   
    public void OnMove(InputValue value){
        var charMove = value.Get<Vector2>();
        Vector3 newSpeed = Vector3.up * collider.linearVelocity.y;
        newSpeed.x = charMove.x * charSpeed;
        newSpeed.z = charMove.y * charSpeed;
        collider.linearVelocity = newSpeed;
    }

    public void OnJump(){
		Vector3 newSpeed = Vector3.zero;
		newSpeed.y += charJumpHeight;
        if(playerInput.actions["Jump"].WasPressedThisFrame())
            collider.linearVelocity = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
