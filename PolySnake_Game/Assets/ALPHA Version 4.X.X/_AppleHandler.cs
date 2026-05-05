using UnityEngine;
// Status: Current Main.

// Version 3.1: Initial implementation of the Apple behavior, including random repositioning on the ground when collected. 4/21/2026
// V-3.2 [Updated - 4/23/2026] - changed the randomization method to use the raycasting from deep space to the center of the geometry,
// which allows it to be used on more complex surfaces like ramps and hills, instead of just flat ground.

// =========================================================================
// COMPATIBILITY: Apple is mainly used in ALPHA Version 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: other apple scripts that may be in the project, and scripts that manage apple behavior in a different way.
// =========================================================================


// This script handles the behavior of the apple, including detecting when the snake eats it and randomizing its position on the ground.
public class Apple : MonoBehaviour
{
    public Transform geometryCenter;

    public float orbitRadius = 200f;
    public LayerMask groundLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snakeHead_Player"))
        {
            if (other.TryGetComponent<OGSnake>(out var ogSnake))
            {
                ogSnake.Grow();
                RandomizePosition();
            }
            else if (other.TryGetComponent<OGSnakeTailHandler>(out var ogTailHandler))
            {
                ogTailHandler.Grow();
                RandomizePosition();
            }
            else if (other.TryGetComponent<IGSnakeTailHandler>(out var iGSnakeTailHandler))
            {
                iGSnakeTailHandler.Grow();
                RandomizePosition();
            }
            else if (other.TryGetComponent<SnakeTailHandler>(out var tailHandler))
            {
                tailHandler.Grow();
                RandomizePosition();
            }
        }
    }

    private void RandomizePosition()
    {
        if (geometryCenter == null)
        {
            Debug.LogError("Apple script needs a Geometry Center assigned to know where to spawn!");
            return;
        }

        Vector3 randomDirection = Random.onUnitSphere;

        Vector3 deepSpacePos = geometryCenter.position + (randomDirection * orbitRadius);

        RaycastHit hit;
        // The ray points in the exact opposite direction (-randomDirection)
        if (Physics.Raycast(deepSpacePos, -randomDirection, out hit, orbitRadius * 2f, groundLayer))
        {
            transform.position = hit.point + (hit.normal * 0.5f);

            transform.up = hit.normal;
        }
        else
        {
            // Failsafe: If the ray somehow misses completely, try again instantly!
            RandomizePosition();
        }
    }
}
