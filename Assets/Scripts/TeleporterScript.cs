using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour {
    private SteamVR_TrackedObject trackedOBJ;
    private SteamVR_Controller.Device device;

    private LineRenderer laser;
    public GameObject teleLocator;

    public GameObject player;
    public LayerMask lyrMask;

    public Color invalidColor;
    public Color validColor;
    
    public List<GameObject> helperDisplays;

    private TeleLocationScript destination;

    private Vector3 teleLocation;
    private RaycastHit hit;

    // Use this for initialization
    void Start () {
        trackedOBJ = GetComponent<SteamVR_TrackedObject>();

        laser = GetComponentInChildren<LineRenderer>();
        if (laser != null)
        {
            laser.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedOBJ.index);

        if (laser != null)
        {

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.gameObject.SetActive(true);
                laser.startColor = invalidColor;
                laser.endColor = invalidColor;
                //teleLocator.SetActive(true);
            }

            if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                laser.SetPosition(0, transform.position);
                if (Physics.Raycast(transform.position, transform.forward, out hit, 20f, lyrMask))
                {
                    laser.SetPosition(1, hit.point);
                    if (destination == null)
                    {
                        destination = hit.collider.gameObject.GetComponent<TeleLocationScript>();
                        if (destination == null) return;
                        destination.ShowDisplay();
                        //teleLocator.transform.position = teleLocation;
                        laser.startColor = validColor;
                        laser.endColor = validColor;
                    }
                }
                else
                {
                    if(destination != null)
                    {
                        destination.HideDisplay();
                        laser.startColor = invalidColor;
                        laser.endColor = invalidColor;
                    }
                    destination = null;
                    laser.SetPosition(1, transform.position + transform.forward * 20f);
                }
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (destination != null)
                {
                    destination.HideDisplay();
                    player.transform.position = destination.GetLocation();
                    if (helperDisplays[0].activeSelf)
                    {
                        foreach(GameObject helper in helperDisplays) helper.SetActive(false);
                    }
                }
                laser.gameObject.SetActive(false);
                destination = null;
                //teleLocator.SetActive(false);
            }
        }
    }
}
