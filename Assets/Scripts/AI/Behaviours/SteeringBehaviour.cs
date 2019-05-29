using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    public float weighting;

    // If you declare a function as abstract then you don't need to write it out since a derived class MUST have an override of this function
    public abstract Vector3 GetForce(AI owner);
}
