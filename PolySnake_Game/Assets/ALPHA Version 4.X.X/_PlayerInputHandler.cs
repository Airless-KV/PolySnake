using UnityEngine;
using UnityEngine.InputSystem;
// Status: Current Main.

// Version 1.1: Initial implementation of the player input handling for the snake, with variable steering speed that accelerates the longer you hold the turn keys. 4/23/2026

// =========================================================================
// COMPATIBILITY: PlayerInputHandler is mainly used in ALPHA Version 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: other player input handlers, and scripts that manage player input in a different way.
// =========================================================================

// This script handles the player's input for controlling the snake's movement.
public class PlayerInputHandler : MonoBehaviour
{
    public float moveSpeed = 10f;

    public float minRotationSpeed = 80f;    // Speed when you first tap the key
    public float maxRotationSpeed = 200f;   // Maximum speed after holding the key
    public float rotationAcceleration = 150f; // How fast it revs up from Min to Max

    private Rigidbody rb;

    // Trackers for our acceleration math
    private float currentRotationSpeed;
    private float lastInputDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentRotationSpeed = minRotationSpeed;
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        float h = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();

        if (h != 0)
        {
            if (lastInputDirection != 0 && Mathf.Sign(h) != Mathf.Sign(lastInputDirection))
            {
                currentRotationSpeed = minRotationSpeed;
            }

            currentRotationSpeed += rotationAcceleration * Time.fixedDeltaTime;

            currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, minRotationSpeed, maxRotationSpeed);

            float turnAmount = h * currentRotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turnAmount, 0);
            rb.MoveRotation(rb.rotation * turnRotation);

            lastInputDirection = h;
        }
        else
        {
            currentRotationSpeed = minRotationSpeed;
            lastInputDirection = 0f;
        }

        Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }
}
