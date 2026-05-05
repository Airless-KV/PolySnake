using UnityEngine;
using UnityEngine.UIElements;
// Status: Current Main.

// Version 1.1: Initial implementation of the AppleCompassHandler. 4/18/2026
// V-1.2 [Updated - 4/23/2026] - added a manual rotation offset to fix the 3D model's broken export axis,
// and added a smoothing effect to the compass needle's movement.

// =========================================================================
// COMPATIBILITY: _APPLCompassHandler is mainly used in ALPHA Version 1.X.X, 2.X.X, 3.X.X, and 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: N/A fully compatible with all other scripts.
// =========================================================================


// This script is attached to the Apple Compass Pointer. It continuously points towards the nearest Apple in the scene.
public class AppleCompassHandler : MonoBehaviour
{
    private Transform targetApple;
    public float rotationSpeed = 15f;
    public Vector3 rotationOffset;

    void Update()
    {
        // 1. Find the nearest Apple if we don't have one already
        if (targetApple == null)
        {
            GameObject appleObj = GameObject.FindGameObjectWithTag("AAPL");
            if (appleObj != null)
            {
                targetApple = appleObj.transform;
            }
            else
            {
                return; 
            }
        }

        // 2. Calculate the raw direction to the Apple
        Vector3 directionToApple = (targetApple.position - transform.position).normalized;

        // 3. We tell the pointer to look at the apple, BUT we force its "Up" vector to exactly match 
        Vector3 flatDirection = Vector3.ProjectOnPlane(directionToApple, transform.parent.up).normalized;

        if (flatDirection != Vector3.zero)
        {
            // 4. Calculate the base rotation (staying flat against the floor)
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection, transform.parent.up);

            // 5. Apply the manual offset to fix the 3D model's broken Blender/export axis
            Quaternion offsetRotation = targetRotation * Quaternion.Euler(rotationOffset);

            // 6. Smoothly swing the needle like a real physical compass
            transform.rotation = Quaternion.Slerp(transform.rotation, offsetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
