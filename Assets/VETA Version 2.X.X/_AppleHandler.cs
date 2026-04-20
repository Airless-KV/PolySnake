using UnityEngine;
// Version 2.1: Initial implementation of the spheroid apple behavior. 4/18/2026

// =========================================================================
// COMPATIBILITY: _AppleHandler is mainly used in VETA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: other apple handlers, and scripts that manage apple behavior in a different way,
// especially those that do not use the 3D sphere/ramp mechanics.
// =========================================================================

// =========================================================================
// NOTE: Change the component name to match the snake tail handler you are using, if you switch to a different one.
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
            other.GetComponent<OGSnakeTailHandler>().Grow();
            RandomizePosition();
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
