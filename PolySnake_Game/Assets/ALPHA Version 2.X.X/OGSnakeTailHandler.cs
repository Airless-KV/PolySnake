using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
// Status: Testing and Debugging only.
// Abbandoned for the use of newer 1GSnakeTailHandler script for better looking movement, but left in the project for reference. 4/19/2026

// Version 1.1: Initial implementation of the snake tail handler, managing tail movement and growth when eating apples. 4/18/2026
// V-1.2 [Updated - 4/21/2026] - changed so that 2D planes work and in the abbsesnce of a planet reference, the loop will not break.  
// Version 1.2.1: Cleaned up, commented, renamed, and refactored version of the original OGSnakeTailHandler script. 4/21/2026

// =========================================================================
// COMPATIBILITY: OGSnakeTailHandler is mainly used in ALPHA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: other snake tail handlers, and scripts that manage tail movement and growth in a different way.
// =========================================================================


// This script is responsible for managing the snake's tail pieces, including their movement to follow the head and growth when eating apples.
public class OGSnakeTailHandler : MonoBehaviour
{
    public GameObject tailPrefab;
    public float tailGap = 1.2f;
    public float moveSpeed = 100f; 
    public Transform planet; 

    private List<Rigidbody> snakeBody = new List<Rigidbody>();

    public int score = 0; 
    void Awake()
    {
        snakeBody.Add(GetComponent<Rigidbody>());
    }

    // Moves the tail pieces to follow the head, while keeping them aligned with the planet's gravity
    void FixedUpdate()
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            Rigidbody currentSegment = snakeBody[i];
            Rigidbody previousSegment = snakeBody[i - 1];

            float distance = Vector3.Distance(currentSegment.position, previousSegment.position);

            if (distance > tailGap)
            {
                // Move towards the piece in front
                Vector3 targetPos = Vector3.MoveTowards(currentSegment.position, previousSegment.position, moveSpeed * Time.fixedDeltaTime);
                currentSegment.MovePosition(targetPos);

                Vector3 segmentGravityDir;

                if (planet != null)
                {
                    segmentGravityDir = (planet.position - currentSegment.position).normalized;
                }
                else
                {
                    segmentGravityDir = Vector3.down;
                }

                // Calculate the forward direction based on the previous segment's position and current segment's position.
                Vector3 forwardDir = (previousSegment.position - currentSegment.position).normalized;
                currentSegment.MoveRotation(Quaternion.LookRotation(forwardDir, -segmentGravityDir));
            }
        }
    }

    // Grows the snake by adding a new tail segment
    public void Grow()
    {
        if (snakeBody.Count == 0)
        {
            snakeBody.Add(GetComponent<Rigidbody>());
        }

        Rigidbody lastSegment = snakeBody[snakeBody.Count - 1];
        Vector3 spawnPosition = lastSegment.position - lastSegment.transform.forward * tailGap;

        GameObject newTail = Instantiate(tailPrefab, spawnPosition, lastSegment.rotation);
        snakeBody.Add(newTail.GetComponent<Rigidbody>());

        score++;
        Debug.Log("Apple eaten! Current Score: " + score);
    }

    // Checks if the snake has collided with itself (ignoring the head and the first tail segment)
    public bool IsSelfCollision(GameObject hitObject)
    {
        return (snakeBody.Count > 1 && hitObject != snakeBody[1].gameObject);
    }
}
