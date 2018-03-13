using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingScript : MonoBehaviour {
    private SteamVR_TrackedObject trackedOBJ;
    private SteamVR_Controller.Device device;
    private GameObject heldObject;
    private Rigidbody heldRB;
    private AxePhysicsScript axe;

    public GameObject model;
    public float throwFactor = 2;

    private float distance;

	// Use this for initialization
	void Awake () {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedOBJ.index);
	}

    private void OnTriggerStay(Collider other)
    {
        if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            GrabAxe(other.gameObject);
        }
        if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            ThrowItem(other.gameObject);
        }
    }

    private void GrabItem(GameObject obj)
    {
        if (heldObject != null) return;
        heldObject = obj;
        heldObject.transform.SetParent(transform);
        heldRB = heldObject.GetComponent<Rigidbody>();
        axe = heldObject.GetComponent<AxePhysicsScript>();
        if(axe != null)
        {
            distance = Vector3.Distance(transform.position, heldObject.transform.position);
            heldObject.transform.position = transform.position + transform.forward * distance;
            heldObject.transform.localEulerAngles = Vector3.zero;
        }
        heldRB.isKinematic = true;
        heldRB.useGravity = false;
        model.SetActive(false);
    }

    private GhostAxeScript ghost;

    private void GrabAxe(GameObject grabedAxe)
    {
        ghost = grabedAxe.GetComponent<GhostAxeScript>();
        if (ghost == null)
        {
            heldObject = grabedAxe;
            distance = Vector3.Distance(transform.position, heldObject.transform.position);
        }
        else
        {
            heldObject = ghost.GrabAxe();
            distance = Vector3.Distance(transform.position, ghost.transform.position);
        }
        heldObject.transform.SetParent(transform);
        heldRB = heldObject.GetComponent<Rigidbody>();
        axe = heldObject.GetComponent<AxePhysicsScript>();
        heldObject.transform.position = transform.position + transform.forward * distance;
        heldObject.transform.localEulerAngles = Vector3.zero;
        heldRB.isKinematic = true;
        heldRB.useGravity = false;
        heldRB.detectCollisions = true;
        model.SetActive(false);
    }

    private void ThrowItem(GameObject obj)
    {
        if (obj.transform.parent != transform) return;
        heldRB.isKinematic = false;
        heldRB.useGravity = true;
        if (axe == null)
        {
            heldRB.velocity = device.velocity * throwFactor;
            heldRB.angularVelocity = device.angularVelocity;
        }
        else
        {
            axe.ThrowAxe(transform.position, device.velocity * throwFactor, device.angularVelocity);
            axe = null;
        }
        heldObject.transform.SetParent(null);
        heldRB = null;
        heldObject = null;
        model.SetActive(true);
    }
}
