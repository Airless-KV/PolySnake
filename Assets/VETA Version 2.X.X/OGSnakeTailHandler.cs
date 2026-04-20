using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
// Version 1.?: Initial implementation of the snake tail handler, managing tail movement and growth when eating apples. 4/18/2026
// Abbandoned for the use of newer _SnakeTailHandler script for better looking movement, but left in the project for reference. 4/19/2026
// Version 1.?.1: Cleaned up, commented, renamed, and refactored version of the original OGSnakeTailHandler script. 4/19/2026

// =========================================================================
// COMPATIBILITY: OGSnakeTailHandler is mainly used in VETA Version 2.X.X scripts
// NON COMPATIBLE SCRIPTS: other snake tail handlers, and scripts that manage tail movement and growth in a different way.
// =========================================================================

// =========================================================================
// NOTE: Make sure to change the component name in the Apple script's Grow() method when switching to a different snake tail handler script.
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
        if (planet == null) return;

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

                // Look at the piece in front, but keep 'Up' aligned with the planet!
                Vector3 segmentGravityDir = (planet.position - currentSegment.position).normalized;
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
