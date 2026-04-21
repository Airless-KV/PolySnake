using UnityEngine;
// Status: Current Main.

// Version 1.1: Initial implementation of the AppleCompassHandler. 4/18/2026

// =========================================================================
// COMPATIBILITY: _APPLCompassHandler is mainly used in ALPHA Version 1.X.X, 2.X.X scripts.
// NON COMPATIBLE SCRIPTS: N/A fully compatible with all other scripts.
// =========================================================================

// NOTICE == cant tell if compass is level to the layer. | NEED FIX |

// This script is attached to the Apple Compass Pointer. It continuously points towards the nearest Apple in the scene.
public class AppleCompassHandler : MonoBehaviour
{
    private Transform targetApple;

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
        transform.rotation = Quaternion.LookRotation(directionToApple, transform.parent.up); 
    }
}
