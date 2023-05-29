using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VrPlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference sprintActionReference;
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private CharacterController controller;

    // Jumping
    [SerializeField] private float jumpForce = 8f; // Force applied when jumping
    private bool isJumping = false;
    private float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        jumpActionReference.action.performed += OnJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float currentJumpForce = jumpForce;
        Vector3 moveDirection = Vector3.zero;

        while (isJumping && currentJumpForce > 0f)
        {
            // Gradually decrease the upward force over time
            currentJumpForce -= gravity * Time.deltaTime;

            // Apply upward force
            moveDirection.y = currentJumpForce;

            // Move the character controller
            controller.Move(moveDirection * Time.deltaTime);

            yield return null;
        }

        isJumping = false;
    }
}