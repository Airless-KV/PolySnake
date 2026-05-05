using UnityEngine;
// Status: Testing and Debugging only.
// Abbandoned for the use of newer AppleHandler script that is more modular on what surfaces it can be used on, but left in the project for reference. 4/21/2026

// Version 2.1: Initial implementation of the spheroid apple behavior. 4/18/2026
// V-2.2 [Updated - 4/21/2026] - added other snake tail handler growth calls in the OnTriggerEnter method.

// =========================================================================
// COMPATIBILITY: _AppleHandler is mainly used in ALPHA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: other apple handlers, and scripts that manage apple behavior in a different way,
// especially those that do not use the 3D sphere/ramp mechanics.
// =========================================================================


// This script handles the behavior of the spheroid apple, including detecting when the snake eats it and randomizing its position.
public class AppleHandler : MonoBehaviour
{
    public Transform planet;
    public float planetRadius = 50f;

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


    private void RandomizePosition()
    {

        Vector3 randomDirection = Random.onUnitSphere;
        Vector3 spawnPoint = planet.position + (randomDirection * planetRadius);

        transform.position = spawnPoint;
        transform.up = randomDirection;
    }

} 
