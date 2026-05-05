using UnityEngine;
// Status: Testing and Debugging only.
// Abbandoned for the use of _GravityReceiverHandler and _PlanetGravityHandler scripts for more control, but left in the project for reference. 4/19/2026

// Version 1.?: Initial implementation of the faux gravity handler. 4/18/2026
// Version 1.?.1: Cleaned up, commented, renamed, and refactored version of the original FauxGravityHandler script. 4/19/2026

// =========================================================================
// COMPATIBILITY: OGFauxGravityHandler is mainly used in ALPHA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: OGApple
// =========================================================================


// This script is responsible for simulating gravity towards a central planet and aligning the object upright relative to the planet's surface.
public class OGFauxGravityHandler : MonoBehaviour
{
    public Transform planet;
    public float gravitySpeed = 9.8f;
    public float surfaceRotationSpeed = 10f;

    // The head needs to align upright, but the tail aligns to the piece in front of it.
    public bool alignUpright = true;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
    }

    void FixedUpdate()
    {
        if (planet == null) return;

        // 1. Pull down to the planet
        Vector3 gravityDir = (planet.position - rb.position).normalized;
        rb.AddForce(gravityDir * gravitySpeed, ForceMode.Acceleration);

        // 2. Align upright (Only runs if the checkbox is checked in the Inspector)
        if (alignUpright)
        {
            Vector3 targetUp = -gravityDir;
            Quaternion targetSurfaceRotation = Quaternion.FromToRotation(transform.up, targetUp) * rb.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetSurfaceRotation, surfaceRotationSpeed * Time.fixedDeltaTime));
        }
    }
}
