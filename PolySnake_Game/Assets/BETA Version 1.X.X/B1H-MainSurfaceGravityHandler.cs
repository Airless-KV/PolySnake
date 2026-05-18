using UnityEngine;

public class MainSurfaceGravityHandler : MonoBehaviour
{
    [Header("Performance Settings")]
    [Tooltip("How many rays to shoot. 1 = Down only (Fast). 5 = Down + Diagonals. 9 = Full Starburst (Heavy).")]
    [Range(1, 9)]
    public int activeRaycasts = 9;

    [Header("Gravity Settings")]
    public float gravityForce = 20f;
    public float rayLength = 3f;
    public LayerMask groundLayer;
    public bool alignUpright = true;

    public float maxAlignSpeed = 25f;
    public float minAlignSpeed = 2f;

    public bool showDebugRays = true;

    public float edgeWrapSpeed = 180f;
    private Rigidbody rb;

    private Vector3[] starburstDirections;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        starburstDirections = new Vector3[]
        {
            -Vector3.up,                                  // 1: Straight Down (Belly)
            (-Vector3.up + Vector3.forward).normalized,   // 2: Down-Forward
            (-Vector3.up - Vector3.forward).normalized,   // 3: Down-Backward
            (-Vector3.up + Vector3.right).normalized,     // 4: Down-Right
            (-Vector3.up - Vector3.right).normalized,     // 5: Down-Left
            Vector3.forward,                              // 6: Straight Forward (Nose)
            -Vector3.forward,                             // 7: Straight Backward (Tail)
            Vector3.right,                                // 8: Right
            -Vector3.right                                // 9: Left
        };
    }

    void FixedUpdate()
    {
        Vector3 bestNormal = Vector3.zero;
        bool hitFound = false;

        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < activeRaycasts; i++)
        {
            Vector3 localDir = starburstDirections[i];

            Vector3 worldDir = transform.TransformDirection(localDir);

            if (showDebugRays)
            {
                Debug.DrawRay(transform.position, worldDir * rayLength, Color.green);
            }

            float dynamicRayLength = Mathf.Max(rayLength, MainGameBootManager.CurrentMapSize);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, worldDir, out hit, dynamicRayLength, groundLayer))
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    bestNormal = hit.normal;
                    hitFound = true;
                }
            }
        }

        if (hitFound)
        {
            float dynamicGravity = gravityForce * 10f; 
            rb.AddForce(-bestNormal * dynamicGravity, ForceMode.Acceleration);

            if (alignUpright)
            {
                float angleDifference = Vector3.Angle(transform.up, bestNormal);
                float currentAlignSpeed = Mathf.Lerp(minAlignSpeed, maxAlignSpeed, angleDifference / 45f);

                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, bestNormal) * transform.rotation;
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, currentAlignSpeed * Time.fixedDeltaTime));
            }
        }
        else
        {
            Vector3 recoveryDirection = Vector3.down; // Default for flat maps
            
            if (MainGameBootManager.CurrentMapName.Contains("Sphere") || MainGameBootManager.CurrentMapName.Contains("Cube"))
            {
                recoveryDirection = (Vector3.zero - transform.position).normalized;
            }

            rb.AddForce(recoveryDirection * gravityForce * 2f, ForceMode.Acceleration);

            if (alignUpright)
            {
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -recoveryDirection) * transform.rotation;
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, maxAlignSpeed * Time.fixedDeltaTime));
            }
        }
    }
}