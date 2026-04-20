using UnityEngine;

public class GravityReceiverHandler : MonoBehaviour
{
    public PlanetGravityHandler myPlanet; 
    public bool alignUpright = true; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Turn off standard Unity gravity
    }

    void FixedUpdate()
    {
        // If we found a planet, ask it to attract us every frame!
        if (myPlanet != null)
        {
            myPlanet.Attract(rb, alignUpright);
        }
    }
}
