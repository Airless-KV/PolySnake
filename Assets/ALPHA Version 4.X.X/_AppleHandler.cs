using UnityEngine;
// Status: Preliminary Testing and Debugging.

// Version 3.1: Initial implementation of the Apple behavior, including random repositioning on the ground when collected. 4/21/2026

// =========================================================================
// COMPATIBILITY: Apple is mainly used in ALPHA Version 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: other apple scripts that may be in the project, and scripts that manage apple behavior in a different way.
// =========================================================================


// This script handles the behavior of the apple, including detecting when the snake eats it and randomizing its position on the ground.
public class Apple : MonoBehaviour
{
    public float spawnAreaLimit = 40f; 
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
        // 1. Pick a random spot from a bird's eye view
        float randomX = Random.Range(-spawnAreaLimit, spawnAreaLimit);
        float randomZ = Random.Range(-spawnAreaLimit, spawnAreaLimit);

        // 2. Go high up in the sky at that spot
        Vector3 skyPosition = new Vector3(randomX, 100f, randomZ);

        // 3. Shoot a laser straight down
        RaycastHit hit;
        if (Physics.Raycast(skyPosition, Vector3.down, out hit, 200f, groundLayer))
        {
            // If we hit the ground/ramp, move the apple exactly to the surface and stand it upright!
            transform.position = hit.point + new Vector3(0, 0.5f, 0); // 0.5f lifts it slightly off the dirt
            transform.up = hit.normal;
        }
    }
}
