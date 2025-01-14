using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    public PlayerInput.OnFootActions OnFoot => onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    void Awake()
    {
        // Initialize the PlayerInput and OnFoot actions
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        // Get motor and look components
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        // Assign input actions
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }
    void FixedUpdate()
    {
        // Process movement
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        // Process look
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}