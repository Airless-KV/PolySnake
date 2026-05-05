using System.Collections.Generic;
using UnityEngine;
// Status: Testing and Debugging only.
// Abbandoned for the use of newer _SnakeTailHandler script for its raycast-based movement and improved mechanics, but left in the project for reference. 4/21/2026

// Version 2.1: second implementation of the snake tail handler, with improved movement and growth mechanics. 4/19/2026
// V-2.2 [Updated - 4/21/2026] - changed so that 2D planes work and in the abbsesnce of a planet reference, the loop will not break.

// =========================================================================
// COMPATIBILITY: SnakeTailHandler is mainly used in ALPHA Version 3.X.X scripts
// NON COMPATIBLE SCRIPTS: other snake tail handlers, and scripts that manage tail movement and growth in a different way.
// =========================================================================


// This script is responsible for managing the snake's tail pieces, including their movement to follow the head and growth when eating apples.
// It also includes improved mechanics to ensure smoother movement and prevent jittering.
public class IGSnakeTailHandler : MonoBehaviour
{
    public GameObject tailPrefab;
    public float tailGap = 1.2f;
    public float moveSpeed = 10f;

    public PlanetGravityHandler myPlanet;

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

            // 1. Look at the piece in front (Steering)
            Vector3 directionToFront = (previousSegment.position - currentSegment.position).normalized;
            Vector3 segmentGravityDir;

            if (myPlanet != null)
            {
                // If true: Point towards the planet
                segmentGravityDir = (myPlanet.transform.position - currentSegment.position).normalized;
            }
            else
            {
                // If false: Point straight down
                segmentGravityDir = Vector3.down;
            }

            Quaternion targetRotation = Quaternion.LookRotation(directionToFront, -segmentGravityDir);

            // We use Slerp here to turn the "steering wheel" smoothly over time. This creates the snake curve!
            currentSegment.MoveRotation(Quaternion.Slerp(currentSegment.rotation, targetRotation, moveSpeed * Time.fixedDeltaTime));

            // 2. Drive Forward (Gas Pedal)
            if (distance > tailGap)
            {
                float distanceToClose = distance - tailGap;

                float moveStep = Mathf.Min(moveSpeed * Time.fixedDeltaTime, distanceToClose);

                Vector3 forwardMove = currentSegment.transform.forward * moveStep;
                currentSegment.MovePosition(currentSegment.position + forwardMove);
            }
        }
    }

    public void Grow()
    {
        if (snakeBody.Count == 0)
        {
            snakeBody.Add(GetComponent<Rigidbody>());
        }

        Rigidbody lastSegment = snakeBody[snakeBody.Count - 1];
        Vector3 spawnPosition = lastSegment.position - lastSegment.transform.forward * tailGap;

        GameObject newTail = Instantiate(tailPrefab, spawnPosition, lastSegment.rotation);


        GravityReceiverHandler tailGravity = newTail.GetComponent<GravityReceiverHandler>();
        if (tailGravity != null)
        {
            tailGravity.myPlanet = this.myPlanet;
            tailGravity.alignUpright = false; // Force this to false so the tail can still point forward
        }

        snakeBody.Add(newTail.GetComponent<Rigidbody>());

        score++;
        Debug.Log("Apple eaten! Current Score: " + score);
    }

    public bool IsSelfCollision(GameObject hitObject)
    {
        return (snakeBody.Count > 1 && hitObject != snakeBody[1].gameObject);
    }
}
