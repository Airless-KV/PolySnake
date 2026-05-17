using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerInputHandler : MonoBehaviour
{
    public float moveSpeed = 15f;

    public float minRotationSpeed = 80f;
    public float maxRotationSpeed = 200f;
    public float rotationAcceleration = 150f;

    private Rigidbody rb;

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

        Vector3 forwardVelocity = transform.forward * moveSpeed;
        
        Vector3 verticalVelocity = Vector3.Project(rb.linearVelocity, transform.up);
        
        rb.linearVelocity = forwardVelocity + verticalVelocity;
    }
}
