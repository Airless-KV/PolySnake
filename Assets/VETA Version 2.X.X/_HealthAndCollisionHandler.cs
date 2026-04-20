using UnityEngine;
using UnityEngine.SceneManagement;
// Version 1.1: Initial implementation of the health and collision handling for the snake. 4/18/2026

// =========================================================================
// COMPATIBILITY: HealthAndCollisionHandler is mainly used in VETA Version 2.X.X scripts.
// NON COMPATIBLE SCRIPTS: other health and collision handlers, and scripts that manage collisions and health in a different way.a
// =========================================================================


// This script handles the snake's health and collision detection and death.
public class HealthAndCollisionHandler : MonoBehaviour
{
    private SnakeTailHandler tailManager;

    void Start()
    {
        // Grab the Tail Manager script so we can talk to it
        tailManager = GetComponent<SnakeTailHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallCollision_Death"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("snakeBodyColilision_Death"))
        {
            // Ask the Tail Manager if this was a valid kill-shot
            if (tailManager != null && tailManager.IsSelfCollision(collision.gameObject))
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Game Over! You hit a " + gameObject.name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
