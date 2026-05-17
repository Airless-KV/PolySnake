using UnityEngine;

public class MainApple : MonoBehaviour
{
    public enum MapMode { Spherical3D, Flat2D }

    [Header("Map Settings")]
    [Tooltip("Choose whether this level is a 3D Planet or a 2D Flat Arena.")]
    public MapMode currentMapMode = MapMode.Spherical3D;
    public LayerMask groundLayer;
    public Transform geometryCenter;

    [Header("3D Spherical Settings")]
    [Tooltip("How far out into space to shoot the raycast back at the planet.")]
    public float orbitRadius = 200f;

    [Header("2D Flat Settings")]
    [Tooltip("The X and Z size of your flat map. The Apple will spawn within this box.")]
    public Vector2 flatMapSize = new Vector2(50f, 50f);
    [Tooltip("How high up in the sky the raycast should start from to find the floor.")]
    public float flatSkyHeight = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("snakeHead_Player"))
        {
            if (other.TryGetComponent<MainSnakeTailHandler>(out var MainTailHandler))
            {
                MainTailHandler.Grow();
                RandomizePosition();
            }
        }
    }

    public void RandomizePosition()
    {
        if (geometryCenter == null)
        {
            Debug.LogError("Apple script needs a Geometry Center assigned to know where to spawn!");
            return;
        }

        int maxAttempts = 30;

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 rayOrigin = Vector3.zero;
            Vector3 rayDirection = Vector3.zero;

            if (currentMapMode == MapMode.Spherical3D)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                rayOrigin = geometryCenter.position + (randomDirection * orbitRadius);
                rayDirection = -randomDirection;
            }

            else if (currentMapMode == MapMode.Flat2D)
            {
                float randomX = Random.Range(-flatMapSize.x / 2f, flatMapSize.x / 2f);
                float randomZ = Random.Range(-flatMapSize.y / 2f, flatMapSize.y / 2f);

                rayOrigin = geometryCenter.position + new Vector3(randomX, flatSkyHeight, randomZ);
                rayDirection = Vector3.down;
            }

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, orbitRadius * 2f, groundLayer))
            {
                float appleOffset = 0.6f;
                transform.position = hit.point + (hit.normal * appleOffset);
                transform.up = hit.normal;
                return;
            }
        }
        Debug.LogError("CRITICAL: Apple missed the map 30 times! Check your Ground Layer, Orbit Radius, or Flat Map Size.");
    }
}

