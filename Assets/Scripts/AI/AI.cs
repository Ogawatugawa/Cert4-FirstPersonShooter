using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float maxVelocity = 15f;
    public float maxDistance = 10f;
    public Vector3 velocity;
    public SteeringBehaviour[] behaviours;

    protected NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviours = GetComponents<SteeringBehaviour>();
    }

    private void Update()
    {
        // Set force to zero
        Vector3 force = Vector3.zero;

        // Step 1) Loop through all behaviours and get forces
        foreach (var behaviour in behaviours)
        {
            force += behaviour.GetForce(this);
        }

        // Step 2) Apply force to velocity
        velocity += force * Time.deltaTime;

        // Step 3) Limit velocity to max velocity
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }

        // Step 4) Apply velocty to NavMeshAgent
        if (velocity.magnitude > 0)
        {
            // Position per frame (using velocity)
            Vector3 desiredPos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;
            // Check if desired position is within the NavMesh
            if (NavMesh.SamplePosition(desiredPos, out hit, maxDistance, -1))
            {
                // Set the agent's destination to the nav hit point
                agent.SetDestination(hit.position);
            }

        }
    }

}
