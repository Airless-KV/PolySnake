using UnityEngine;
// Version 1.1: Initial implementation of the gravity receiver handler. 4/19/2026

// =========================================================================
// COMPATIBILITY: GravityReceiverHandler is mainly used in VETA Version 3.X.X scripts
// NON COMPATIBLE SCRIPTS: OGApple, OGFauxGravityHandler, and other scripts that manage gravity in a different way.
// =========================================================================


// This script is responsible for receiving gravity from a planet and applying it to the object it's attached to.
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
