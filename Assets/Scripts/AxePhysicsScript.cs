using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePhysicsScript : MonoBehaviour {
    public Rigidbody rigB;
    public float rotationFactor = 1.0f;
    public float maxDistance;
    public float spinningFactor;

    public void ThrowAxe(Vector3 throwOrigin, Vector3 vel, Vector3 angVel)
    {
        float offset = Vector3.Distance(rigB.transform.position, throwOrigin);
        rigB.velocity = vel * (1f + Mathf.Lerp(0f, rotationFactor, offset / maxDistance));
        rigB.angularVelocity = angVel * (1f + Mathf.Lerp(0f, spinningFactor, offset/maxDistance));
    }
}
