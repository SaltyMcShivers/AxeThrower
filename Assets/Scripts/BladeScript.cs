using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour {
    public float angleTolerance = 40f;

    private List<GameObject> hitObjects;
    private float hitAngle;
    private void Start()
    {
        hitObjects = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!hitObjects.Contains(col.gameObject))
        {
            hitObjects.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (hitObjects.Contains(col.gameObject))
        {
            hitObjects.Remove(col.gameObject);
        }
    }

    public bool CollidingWithObject(GameObject go, ContactPoint point)
    {
        if (hitObjects.Contains(go))
        {
            hitAngle = Vector3.Angle(point.normal, transform.parent.position - transform.position);
            Debug.Log(gameObject.name + " to " + go.name + " " + hitAngle.ToString());
            return hitAngle < angleTolerance || hitAngle > 180f - angleTolerance;
        }
        return false;
    }
}
