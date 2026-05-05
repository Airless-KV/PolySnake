using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSnakeTailHandler : MonoBehaviour
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
        if (autoGrowTest)
        {
            growTimer += Time.deltaTime;

            if (growTimer >= growInterval)
            {
                Grow();          
                growTimer = 0f;  
            }
        }
    }

    public bool IsSelfCollision(GameObject hitObject)
    {
        if (!tailPieces.Contains(hitObject)) return false;

        for (int i = 0; i < Mathf.Min(3, tailPieces.Count); i++)
        {
            if (hitObject == tailPieces[i]) return false;
        }

        return true;
    }
}
