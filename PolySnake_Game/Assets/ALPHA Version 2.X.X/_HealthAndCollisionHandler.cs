using UnityEngine;
using UnityEngine.SceneManagement;
// Status: Current Main.

// Version 1.1: Initial implementation of the health and collision handling for the snake. 4/18/2026
// V-1.2 [Updated - 4/24/2026] - fixed a bug where the self-collision would not trigger, and added an optional failsafe to catch wall collisions
// that are triggers instead of solid objects, just in case you ever want to make your walls into triggers later on.

// =========================================================================
// COMPATIBILITY: HealthAndCollisionHandler is mainly used in ALPHA Version 1.X.X, 2.X.X, 3.X.X, and 4.X.X scripts.
// NON COMPATIBLE SCRIPTS: other health and collision handlers, and scripts that manage collisions and health in a different way.a
// =========================================================================


// This script handles the snake's health and collision detection and death.
public class HealthAndCollisionHandler : MonoBehaviour
{
    private SnakeTailHandler tailManager;

    void Start()
    {
        tailManager = GetComponent<SnakeTailHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallCollision_Death"))
        {
            Die("Wall");
        }
    }

    // 2. THIS CATCHES GHOST OBJECTS (Your new IsTrigger Tail Pieces)
    private void OnTriggerEnter(Collider other)
    {
        // 1. CHECK THE TAIL (No Tags Required!)
        if (tailManager != null && tailManager.IsSelfCollision(other.gameObject))
        {
            Die("Tail");
            return;
        }
        // 2. CHECK FOR GHOST HAZARDS
        if (other.gameObject.CompareTag("wallCollision_Death"))
        {
            Die("Wall");
        }
    }

    private void Die(string causeOfDeath)
    {
        Debug.Log("Game Over! The snake crashed into a " + causeOfDeath);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
