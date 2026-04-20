using UnityEngine;

public class GravityReceiverHandler : MonoBehaviour
{
    public PlanetGravityHandler myPlanet; // Drag the Planet here
    public bool alignUpright = true; // True for the Head, False for the Tail

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
