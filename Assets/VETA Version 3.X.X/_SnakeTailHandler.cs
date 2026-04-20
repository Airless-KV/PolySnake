using System.Collections.Generic;
using UnityEngine;

public class SnakeTailHandler : MonoBehaviour
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
        if (myPlanet == null) return;

        for (int i = 1; i < snakeBody.Count; i++)
        {
            Rigidbody currentSegment = snakeBody[i];
            Rigidbody previousSegment = snakeBody[i - 1];

            float distance = Vector3.Distance(currentSegment.position, previousSegment.position);

            // 1. Look at the piece in front (Steering)
            Vector3 directionToFront = (previousSegment.position - currentSegment.position).normalized;
            Vector3 segmentGravityDir = (myPlanet.transform.position - currentSegment.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToFront, -segmentGravityDir);

            // We use Slerp here to turn the "steering wheel" smoothly over time. This creates the snake curve!
            currentSegment.MoveRotation(Quaternion.Slerp(currentSegment.rotation, targetRotation, moveSpeed * Time.fixedDeltaTime));

            // 2. Drive Forward (Gas Pedal)
            if (distance > tailGap)
            {
                // Calculate exactly how many inches we need to move to fix the gap
                float distanceToClose = distance - tailGap;

                // Mathf.Min guarantees we NEVER drive faster than distanceToClose. 
                // This is the magic bullet that completely stops all jitter!
                float moveStep = Mathf.Min(moveSpeed * Time.fixedDeltaTime, distanceToClose);

                // CRITICAL: We move along our OWN forward direction, driving exactly where we are looking!
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
