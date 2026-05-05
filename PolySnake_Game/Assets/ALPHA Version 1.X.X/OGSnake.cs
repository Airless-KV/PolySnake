using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
// Status: Testing and Debugging only.
// Abbandoned in favor of splitting the functionality into separate scripts, but keeping this here for reference. 4/18/2026

// Version 1.?: Initial implementation of the snake movement, growth, and collision detection. 4/18/2026
// Version 1.?.1: Cleaned up, commented, renamed, and refactored version of the original Snake script. 4/19/2026

// =========================================================================
// COMPATIBILITY: OGSnake is mainly used in ALPHA Version 1.X.X scripts.
// NON COMPATIBLE SCRIPTS: not compatible with other player input, tail handler, and health/collision scripts.
// =========================================================================


// This script handles the movement of the snake, growing the snake when it eats an apple, and detecting collisions that would cause the snake to die.
public class OGSnake : MonoBehaviour
{
    // Movement of the snake:
    public Rigidbody snake_Rigidbody;
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    // Growing the snake:
    public GameObject tailPrefab; 
    public float tailGap = 1.2f;  
    private List<Rigidbody> snakeBody = new List<Rigidbody>();

    void Awake()
    {
        snakeBody.Add(this.snake_Rigidbody);
    }

    void FixedUpdate()
    {
        // The snake will always move forward, and the player can steer it left or right using the A and D keys.
        // replaced with _SnakeInputtHandler script, but keeping this here for reference. 4/18/2026
        if (Keyboard.current == null) return;

        float h = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();

        Vector3 forward = transform.forward * moveSpeed * Time.fixedDeltaTime;
        snake_Rigidbody.MovePosition(snake_Rigidbody.position + forward);

        if (h != 0)
        {
            float turnAmount = h * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, turnAmount, 0);
            snake_Rigidbody.MoveRotation(snake_Rigidbody.rotation * deltaRotation);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------

        // This loop makes each segment of the snake's body follow the segment in front of it.
        // replaced with _SnakeTailHandler script, but keeping this here for reference. 4/18/2026
        for (int i = 1; i < snakeBody.Count; i++) 
        {
            Rigidbody currentSegment = snakeBody[i];
            Rigidbody previousSegment = snakeBody[i - 1];

            float distance = Vector3.Distance(currentSegment.position, previousSegment.position);

            // Only move the segment if it's farther than the tailGap distance from the previous segment.
            if (distance > tailGap) 
            {
                Vector3 targetPos = Vector3.MoveTowards(currentSegment.position, previousSegment.position, moveSpeed * Time.fixedDeltaTime);
                
                targetPos.y = currentSegment.position.y;

                currentSegment.MovePosition(targetPos);
                currentSegment.transform.LookAt(previousSegment.position);
            }
        }
    }

    // This method is called when the snake eats an apple. It spawns a new cube at the end of the snake and adds it to the list.
    // replaced with _SnakeTailHandler script, but keeping this here for reference. 4/18/2026
    public void Grow()
    {
        Rigidbody lastSegment = snakeBody[snakeBody.Count - 1];  
        Vector3 spawnPosition = lastSegment.position - lastSegment.transform.forward * tailGap; 

        GameObject newTail = Instantiate(tailPrefab, spawnPosition, lastSegment.rotation); 
        snakeBody.Add(newTail.GetComponent<Rigidbody>());
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------

    // This method is called when the snake collides with something. We check if it's a wall or its own body, and if so, we call the Die() method.
    // replaced with _HealthAndCollision script, but keeping this here for reference. 4/18/2026
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallCollision_Death"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("snakeBodyColilision_Death"))
        {
            if (snakeBody.Count > 1 && collision.gameObject != snakeBody[1].gameObject) 
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
