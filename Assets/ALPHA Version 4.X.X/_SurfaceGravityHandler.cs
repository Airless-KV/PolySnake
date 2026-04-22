using UnityEngine;
// Status: Preliminary Testing and Debugging.

// Version 1.0: Initial implementation of the custom surface gravity script, which allows objects to stick to ramps and hills while driving over them. 4/21/2026

// =========================================================================
// COMPATIBILITY: SurfaceGravity is mainly used in ALPHA Version 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: other gravity scripts that manage gravity in a different way.
// =========================================================================

// This script simulates a custom gravity that allows objects to stick to ramps and hills, providing a more grounded driving experience.
// It uses raycasting to detect the ground and applies forces to keep the object attached to the surface, while also aligning it with the slope of the terrain.
public class SurfaceGravity : MonoBehaviour
{
    public float gravityForce = 20f;
    public float rayLength = 3f;
    public LayerMask groundLayer; 
    public bool alignUpright = true;

    public float maxAlignSpeed = 25f;
    public float minAlignSpeed = 2f;

    public float edgeWrapSpeed = 180f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, groundLayer))
        {
            // 1. Pull the object tightly to the ramp/floor
            rb.AddForce(-hit.normal * gravityForce, ForceMode.Acceleration);

            
            // 2. Tilt the object to perfectly match the angle of the hill
            if (alignUpright)
            {
                float angleDifference = Vector3.Angle(transform.up, hit.normal);

                float currentAlignSpeed = Mathf.Lerp(minAlignSpeed, maxAlignSpeed, angleDifference / 45f);

                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

                // Use our new dynamic 'currentAlignSpeed' instead of the hardcoded 15f!
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, currentAlignSpeed * Time.fixedDeltaTime));
            }
        }
        else
        {
            // If we drive off a cliff, fall straight down like normal gravity
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);

            if (alignUpright)
            {
                // We multiply our current rotation by a new Euler angle to tilt it forward over time.
                Quaternion pitchDown = rb.rotation * Quaternion.Euler(edgeWrapSpeed * Time.fixedDeltaTime, 0f, 0f);
                rb.MoveRotation(pitchDown);
            }
        }
    }
}
