using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    
    public override Vector3 GetForce(AI owner)
    {
        // Set force to ZERO
        Vector3 force = Vector3.zero;

        // If target is not null
        if (target)
        {
            // Set desiredForce to target - current position
            Vector3 desiredForce = target.position - transform.position;
            // Set force to desiredForce normalized * weighting
            force += desiredForce.normalized * weighting;
        }

        // Return force
        return force;
    }
}
