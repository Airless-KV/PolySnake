using UnityEngine;
using UnityEngine.InputSystem;
// Status: Testing and Debugging only.
// Abbandoned in favor of using the new PlayerInputHnadler for its variable turn speed, but keeping this here for reference. 4/23/2026

// Version 1.1: Initial implementation of the player input handling for the snake. 4/18/2026

// =========================================================================
// COMPATIBILITY: PlayerInputHandler is mainly used in ALPHA Version 2.X.X scripts.
// NON COMPATIBLE SCRIPTS: other player input handlers, and scripts that manage player input in a different way.
// =========================================================================


// This script handles the player's input for controlling the snake's movement, allowing the player to steer left and right while the snake always moves forward.
public class OGPlayerInputHandler : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        float h = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();

        // Steer left and right
        if (h != 0)
        {
            float turnAmount = h * rotationSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turnAmount, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // Always drive forward
        Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }
}
