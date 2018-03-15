using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleLocationScript : MonoBehaviour {

    private Vector3 locationToGo;
    
    public GameObject playSpace;

    // Use this for initialization
    void Start () {
        locationToGo = new Vector3(playSpace.transform.position.x, 0f, playSpace.transform.position.z);
        playSpace.SetActive(false);
    }

    public void ShowDisplay()
    {
        playSpace.SetActive(true);
    }

    public void HideDisplay()
    {
        playSpace.SetActive(false);
    }

    public Vector3 GetLocation()
    {
        return locationToGo;
    }
}
