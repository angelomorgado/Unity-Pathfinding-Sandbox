using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VrPlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference sprintActionReference;
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private CharacterController controller;

    // Sprinting
    [SerializeField] private float sprintMultiplier = 1.5f; // Multiplier applied to speed when sprinting
    private bool isSprinting = false;

    // Jumping
    [SerializeField] private float jumpForce = 8f; // Force applied when jumping
    private bool isJumping = false;
    private float gravity = 9.8f;
    private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        sprintActionReference.action.performed += OnSprint;
        sprintActionReference.action.canceled += OnSprintCanceled;
        jumpActionReference.action.performed += OnJump;

        // Get collider from child
        capsuleCollider = GetComponentInChildren<CapsuleCollider>();
    }

    private void OnSprint(InputAction.CallbackContext obj)
    {
        isSprinting = true;
        Debug.Log("Sprinting");
    }

    private void OnSprintCanceled(InputAction.CallbackContext obj)
    {
        isSprinting = false;
        Debug.Log("Stopped sprinting");
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

            // Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the character controller
            controller.Move(moveDirection * Time.deltaTime);

            yield return null;
        }

        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Apply sprint
        float speed = isSprinting ? 10.0f : 5.0f;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
