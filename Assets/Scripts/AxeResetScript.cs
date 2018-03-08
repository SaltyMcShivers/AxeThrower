using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeResetScript : MonoBehaviour {
    private SteamVR_TrackedObject trackedOBJ;
    private SteamVR_Controller.Device device;
    private Rigidbody rigB;

    private Vector3 startPosition;
    private Vector3 startRotation;

    public GameObject axe;

    // Use this for initialization
    void Awake()
    {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();
        rigB = axe.GetComponent<Rigidbody>();
        startPosition = axe.transform.position;
        startRotation = axe.transform.eulerAngles;
        rigB.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedOBJ.index);

        if (device.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
        {
            axe.transform.position = startPosition;
            axe.transform.eulerAngles = startRotation;
            axe.transform.SetParent(null);
            rigB.velocity = Vector3.zero;
            rigB.angularVelocity = Vector3.zero;
            rigB.isKinematic = true;
            rigB.detectCollisions = true;
        }
    }
}
