using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTargetScript : MonoBehaviour {
    public Transform target;

    public float radius;
    public float timeToRotate;
    public float offsetTime;

    private float rotationProgress;

    // Update is called once per frame
    void Update () {
        rotationProgress = (((Time.time + offsetTime) % timeToRotate) / timeToRotate) * 2 * Mathf.PI;
        target.transform.localPosition = new Vector3(Mathf.Cos(rotationProgress), Mathf.Sin(rotationProgress), 0f) * radius;
	}
}
