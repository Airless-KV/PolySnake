using UnityEngine;
using UnityEngine.UIElements;

public class MainAPPLCompassHandler : MonoBehaviour
{
    private Transform targetApple;
    public float rotationSpeed = 15f;
    public Vector3 rotationOffset;

    void Update()
    {
        if (targetApple == null)
        {
            GameObject appleObj = GameObject.FindGameObjectWithTag("AAPL");
            if (appleObj != null)
            {
                targetApple = appleObj.transform;
            }
            else
            {
                return;
            }
        }

        Vector3 directionToApple = (targetApple.position - transform.position).normalized;

        Vector3 flatDirection = Vector3.ProjectOnPlane(directionToApple, transform.parent.up).normalized;

        if (flatDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDirection, transform.parent.up);

            Quaternion offsetRotation = targetRotation * Quaternion.Euler(rotationOffset);

            transform.rotation = Quaternion.Slerp(transform.rotation, offsetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
