using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// Status: Current Main.

// Version 3.1: Initial implementation of the snake tail handler, managing tail movement and growth when eating apples. 4/21/2026
// V-3.2 [Updated - 4/22/2026] - changed so that when the snake jumps a cliff the snake tilts downwards instead of upwards.
// V-3.3 [Updated - 4/23/2026] - changed the movement method to use a raycast-based position history system,
// V-3.4 [Updated - 4/23/2026] - changed to history system to use a distance-based method instead of a time-based method.
// V-3.5 [Updated - 4/24/2026] - fixed a bug where the IsSelfCollision would not trigger.

// =========================================================================
// COMPATIBILITY: SnakeTailHandler is mainly used in ALPHA Version 4.X.X scripts
// NON COMPATIBLE SCRIPTS: other snake tail handlers, and scripts that manage tail movement and growth in a different way.
// ========================================================================


// This script is responsible for managing the snake's tail pieces, including their movement to follow the head and growth when eating apples.
public class SnakeTailHandler : MonoBehaviour
{
    public GameObject tailPrefab;
    public float tailGap = 1.2f;

    private List<Vector3> positionHistory = new List<Vector3>();
    private List<Quaternion> rotationHistory = new List<Quaternion>();

    private List<Rigidbody> snakeBody = new List<Rigidbody>();
    private List<GameObject> tailPieces = new List<GameObject>();

    

    void Awake()
    {
        snakeBody.Add(GetComponent<Rigidbody>());
    }

    void FixedUpdate()
    {
        positionHistory.Insert(0, transform.position);
        rotationHistory.Insert(0, transform.rotation);

        float distanceTracker = 0f;
        int historyIndex = 0;

        for (int i = 0; i < tailPieces.Count; i++)
        {
            while (historyIndex < positionHistory.Count - 1 && distanceTracker < tailGap)
            {
                distanceTracker += Vector3.Distance(positionHistory[historyIndex], positionHistory[historyIndex + 1]);
                historyIndex++;
            }

            tailPieces[i].transform.position = positionHistory[historyIndex];
            tailPieces[i].transform.rotation = rotationHistory[historyIndex];

            distanceTracker = 0f;
        }

        if (positionHistory.Count > 0 && historyIndex < positionHistory.Count - 1)
        {
            int buffer = 10;
            if (positionHistory.Count > historyIndex + buffer)
            {
                positionHistory.RemoveRange(historyIndex + buffer, positionHistory.Count - (historyIndex + buffer));
                rotationHistory.RemoveRange(historyIndex + buffer, rotationHistory.Count - (historyIndex + buffer));
            }
        }

    }

    public void Grow()
    {
        Vector3 spawnPos;
        Quaternion spawnRot;

        if (positionHistory.Count > 0)
        {
            spawnPos = positionHistory[positionHistory.Count - 1];
            spawnRot = rotationHistory[rotationHistory.Count - 1];
        }
        else
        {
            spawnPos = transform.position;
            spawnRot = transform.rotation;
        }

        GameObject newTail = Instantiate(tailPrefab, spawnPos, spawnRot);
        tailPieces.Add(newTail);

        //  send score event instead of storing it here
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddScore(1);
    }

    [Header("Performance Test")]
    [Tooltip("Check this box to automatically grow the snake over time.")]
    public bool autoGrowTest = false;
    [Tooltip("How many seconds between each automatic growth.")]
    public float growInterval = 1f;

    private float growTimer = 0f;
    void Update()
    {
        // Only run the timer if the checkbox is ticked in the Inspector
        if (autoGrowTest)
        {
            growTimer += Time.deltaTime; // Time.deltaTime counts up in real-world seconds

            if (growTimer >= growInterval)
            {
                Grow();          // Call your existing Grow function!
                growTimer = 0f;  // Reset the clock for the next piece
            }
        }
    }

    public bool IsSelfCollision(GameObject hitObject)
    {
        // Check if the object we hit is actually in our tail list
        if (!tailPieces.Contains(hitObject)) return false;

        // Ignore the first 3 pieces (the neck/shoulders) to prevent accidental instant-death on tight turns
        for (int i = 0; i < Mathf.Min(3, tailPieces.Count); i++)
        {
            if (hitObject == tailPieces[i]) return false;
        }

        // If it's a tail piece, and it's NOT one of the first 3, it's a lethal hit!
        return true;
    }
}
