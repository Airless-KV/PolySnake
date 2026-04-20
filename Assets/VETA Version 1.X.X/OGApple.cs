using UnityEngine;
// Version 1.2: Initial implementation of the apple behavior. 4/18/2026
// Abbandoned for the 3D sphere/ramp math, but left in the project for reference. 4/18/2026
// Version 1.2.1: Cleaned up, commented, renamed, and refactored version of the original Apple script. 4/20/2026

// =========================================================================
// COMPATIBILITY: OGApple is mainly used in VETA Version 1.X.X scripts.
// NON COMPATIBLE SCRIPTS: not compatible with custom gravity or 3D sphere/ramp mechanics and other apple scripts that may be in the project.
// =========================================================================

// =========================================================================
// NOTE: Change the component name to match the snake tail handler you are using, if you switch to a different one.
// =========================================================================


// This script handles the behavior of the apple, including detecting when the snake eats it and randomizing its position.
public class OGApple : MonoBehaviour
{
    public float spawnRange = 23f; 

    // This method is called when another collider enters the trigger collider attached to the apple.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("The apple was touched by: " + other.gameObject.name);

        if (other.CompareTag("snakeHead_Player"))
        {
            other.GetComponent<OGSnake>().Grow();
            RandomizePosition();
        }
    }

    // This method will move the apple to a random position within a 20x20 area centered around the origin (0,0,0).
    private void RandomizePosition()
    {

        float randomX = Random.Range(spawnRange - spawnRange * 2, spawnRange);
        float randomZ = Random.Range(spawnRange - spawnRange * 2, spawnRange);

        // Move the apple. but keep the Y coordinate at 0 to stay on the ground
        transform.position = new Vector3(randomX, 0f, randomZ);
    }
}  