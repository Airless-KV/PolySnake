using UnityEngine;
// Status: Testing and Debugging only.
// Abbandoned for the 3D sphere/ramp math, but left in the project for reference. 4/18/2026

// Version 1.0: Initial implementation of the apple behavior. 4/18/2026
// V-1.2 [Updated - 4/21/2026] - added public float spawnranege, updated the random positioning to use float,
// added debug log and added other snake tail handler growth calls in the OnTriggerEnter method.
// Version 1.2.1: Cleaned up, commented, renamed, and refactored version of the original Apple script. 4/21/2026

// =========================================================================
// COMPATIBILITY: OGApple is mainly used in ALPHA Version 1.X.X scripts.
// NON COMPATIBLE SCRIPTS: not compatible with custom gravity or 3D sphere/ramp mechanics and other apple scripts that may be in the project.
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

    // This method will move the apple to a random position within a 20x20 area centered around the origin (0,0,0).
    private void RandomizePosition()
    {

        float randomX = Random.Range(spawnRange - spawnRange * 2, spawnRange);
        float randomZ = Random.Range(spawnRange - spawnRange * 2, spawnRange);

        // Move the apple. but keep the Y coordinate at 0 to stay on the ground
        transform.position = new Vector3(randomX, 0f, randomZ);
    }
}  