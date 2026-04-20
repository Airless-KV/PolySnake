using UnityEngine;
// Version 1.1: Initial implementation of the planet gravity handler. 4/19/2026

// =========================================================================
// COMPATIBILITY: PlanetGravityHandler is mainly used in VETA Version 3.X.X scripts
// NON COMPATIBLE SCRIPTS: OGApple, OGFauxGravityHandler, and other scripts that manage gravity in a different way.
//=========================================================================


// This script is responsible for simulating gravity towards the planet and aligning objects upright relative to the planet's surface.
public class PlanetGravityHandler : MonoBehaviour
{
    public float gravitySpeed = 9.8f;
    public float surfaceRotationSpeed = 10f;

    // Any object can call this method and pass in its Rigidbody to get pulled!
    public void Attract(Rigidbody bodyToPull, bool alignUpright)
    {
        // 1. Pull the object down toward the center of THIS planet
        Vector3 gravityDir = (transform.position - bodyToPull.position).normalized;
        bodyToPull.AddForce(gravityDir * gravitySpeed, ForceMode.Acceleration);

        // 2. Align the object upright (if requested)
        if (alignUpright)
        {
            Vector3 targetUp = -gravityDir;
            Quaternion targetSurfaceRotation = Quaternion.FromToRotation(bodyToPull.transform.up, targetUp) * bodyToPull.rotation;
            bodyToPull.MoveRotation(Quaternion.Slerp(bodyToPull.rotation, targetSurfaceRotation, surfaceRotationSpeed * Time.fixedDeltaTime));
        }
    }
}
