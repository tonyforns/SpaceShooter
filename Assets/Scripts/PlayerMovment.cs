using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;

    [SerializeField] private float maxSpeed = 20f;

    [SerializeField] private InputActionReference moveAction;
    private void OnEnable()
    {
        moveAction.action.Enable();
    }


    private void OnDisable()
    {
        moveAction.action.Disable();
    }
    private void Update()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>(); 
        Vector2 moveVelocity = moveInput.normalized * acceleration * Time.deltaTime;
        moveVelocity = Vector2.ClampMagnitude(moveVelocity, maxSpeed);

        transform.Translate(moveVelocity);
    }
}
