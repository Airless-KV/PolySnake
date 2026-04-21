using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// Status: Preliminary Testing and Debugging.

// Version 3.0: Initial implementation of the snake tail handler, managing tail movement and growth when eating apples. 4/21/2026

// =========================================================================
// COMPATIBILITY: SnakeTailHandler is mainly used in ALPHA Version 4.X.X scripts
// NON COMPATIBLE SCRIPTS: other snake tail handlers, and scripts that manage tail movement and growth in a different way.
// ========================================================================


// This script is responsible for managing the snake's tail pieces, including their movement to follow the head and growth when eating apples.
public class SnakeTailHandler : MonoBehaviour
{
    public GameObject tailPrefab;
    public float tailGap = 1.2f;
    public float moveSpeed = 15f;

    private List<Rigidbody> snakeBody = new List<Rigidbody>();
    public int score = 0;

    void Awake()
    {
        snakeBody.Add(GetComponent<Rigidbody>());
    }

    void FixedUpdate()
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            Rigidbody currentSegment = snakeBody[i];
            Rigidbody previousSegment = snakeBody[i - 1];

            float distance = Vector3.Distance(currentSegment.position, previousSegment.position);

            // 1. Steer towards the piece in front
            Vector3 directionToFront = (previousSegment.position - currentSegment.position).normalized;
            Vector3 flatDirection = Vector3.ProjectOnPlane(directionToFront, currentSegment.transform.up).normalized;

            if (flatDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatDirection, currentSegment.transform.up);
                currentSegment.MoveRotation(Quaternion.Slerp(currentSegment.rotation, targetRotation, moveSpeed * Time.fixedDeltaTime));
            }
            // 2. Drive Forward (Gas Pedal)
            if (distance > tailGap)
            {
                float distanceToClose = distance - tailGap;
                float moveStep = Mathf.Min(moveSpeed * Time.fixedDeltaTime, distanceToClose);

                // Drive forward along the ramp
                Vector3 forwardMove = currentSegment.transform.forward * moveStep;
                currentSegment.MovePosition(currentSegment.position + forwardMove);
            }
        }
    }

    public void Grow()
    {
        if (snakeBody.Count == 0) snakeBody.Add(GetComponent<Rigidbody>());

        Rigidbody lastSegment = snakeBody[snakeBody.Count - 1];
        Vector3 spawnPosition = lastSegment.position - lastSegment.transform.forward * tailGap;

        GameObject newTail = Instantiate(tailPrefab, spawnPosition, lastSegment.rotation);
        snakeBody.Add(newTail.GetComponent<Rigidbody>());

        score++;
        Debug.Log("Apple eaten! Current Score: " + score);
    }

    public bool IsSelfCollision(GameObject hitObject)
    {
        return (snakeBody.Count > 1 && hitObject != snakeBody[1].gameObject);
    }
}
