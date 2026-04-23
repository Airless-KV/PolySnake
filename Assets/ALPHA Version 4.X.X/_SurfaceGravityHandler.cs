using UnityEngine;
// Status: Preliminary Testing and Debugging.

// Version 1.1: Initial implementation of the custom surface gravity script, which allows objects to stick to ramps and hills while driving over them. 4/21/2026
// V-1.2 [Updated - 4/22/2026] - added dynamic softlock for aligning with slopes, and added edge wrap to prevent getting stuck on edges of ramps.
// V-1.3 [Updated - 4/23/2026] - added more raycast directions to better handle sticking to walls and ceilings, and improved the logic for finding the best surface normal to stick to.

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

    public bool showDebugRays = true; 

    public float edgeWrapSpeed = 180f;
    private Rigidbody rb;

    private Vector3[] starburstDirections;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        starburstDirections = new Vector3[]
        {
            -Vector3.up,                                  // Straight Down (Belly)
            (-Vector3.up + Vector3.forward).normalized,   // Down-Forward
            Vector3.forward,                              // Straight Forward (Nose)
            (-Vector3.up - Vector3.forward).normalized,   // Down-Backward
            -Vector3.forward,                             // Straight Backward (Tail)
            (-Vector3.up + Vector3.right).normalized,     // Down-Right
            (-Vector3.up - Vector3.right).normalized,     // Down-Left
            Vector3.right,                                // Right
            -Vector3.right                                // Left
        };
    }

    void FixedUpdate()
    {
        Vector3 bestNormal = Vector3.zero;
        bool hitFound = false;

        // We set this to infinity so the first wall we hit becomes the "closest" wall
        float closestDistance = Mathf.Infinity;

        // Shoot lasers in ALL 8 directions to find the nearest floor/wall
        foreach (Vector3 localDir in starburstDirections)
        {
            // Convert local direction to a world direction based on how the snake is currently rotated
            Vector3 worldDir = transform.TransformDirection(localDir);

            if (showDebugRays)
            {
                Debug.DrawRay(transform.position, worldDir * rayLength, Color.green);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, worldDir, out hit, rayLength, groundLayer))
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    bestNormal = hit.normal;
                    hitFound = true;
                }
            }
        }

        if (hitFound)
        {
            rb.AddForce(-bestNormal * gravityForce, ForceMode.Acceleration);

            if (alignUpright)
            {
                float angleDifference = Vector3.Angle(transform.up, bestNormal);
                float currentAlignSpeed = Mathf.Lerp(minAlignSpeed, maxAlignSpeed, angleDifference / 45f);

                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, bestNormal) * transform.rotation;
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, currentAlignSpeed * Time.fixedDeltaTime));
            }
        }
        else
        {
            // If ALL lasers miss (caught massive air off a ramp), fall straight down to the earth
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
    }
}

