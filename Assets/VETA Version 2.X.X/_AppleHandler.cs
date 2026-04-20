using UnityEngine;
// Version 2.1: Initial implementation of the spheroid apple behavior. 4/18/2026

// =========================================================================
// COMPATIBILITY: _AppleHandler is mainly used in VETA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: other apple handlers, and scripts that manage apple behavior in a different way,
// especially those that do not use the 3D sphere/ramp mechanics.
// =========================================================================

// =========================================================================
// NOTE: Other apple scripts SHOULD BE TURNED OFF! multiple scrips will cause conflicts. To turn the 3D Sphere/Ramp math back on remove the comment below.
// DO NOT REMOVE the other apple scripts. Just comment them out!
// NOTE: Change the component name to match the snake tail handler you are using, if you switch to a different one.
// =========================================================================

// =========================================================================
// CLARIFICATION: ON NEEDING TO COMMENT OUT OTHER APPLE SCRIPTS
// If you have multiple Apple scripts in your project, they will all try to run at the same time and cause conflicts.
// Unchecking a script in Unity only stops its frame-by-frame updates, but it does not disable its physics triggers.
// Meaning the code still fires the exact millisecond something collides with it.
// it is the developer decision to leave the other Apple scripts in the component list for reference, but they must be commented out to prevent conflicts.
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
            other.GetComponent<OGSnake>().Grow();
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
