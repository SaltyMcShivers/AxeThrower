using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAxeScript : MonoBehaviour {
    public GameObject axe;
    public GameManagerScript game;

    private Rigidbody rigB;

	// Use this for initialization
	void Start () {
        rigB = axe.GetComponent<Rigidbody>();
        axe.transform.position = transform.position;
        axe.transform.eulerAngles = transform.eulerAngles;
        axe.transform.SetParent(null);
        rigB.velocity = Vector3.zero;
        rigB.angularVelocity = Vector3.zero;
        rigB.isKinematic = true;
        rigB.detectCollisions = true;
    }

    public GameObject GrabAxe()
    {
        if(game != null)
        {
            game.StartGame();
        }
        return axe;
    }

    public void ResetAxe()
    {
        axe.transform.position = transform.position;
        axe.transform.eulerAngles = transform.eulerAngles;
        axe.transform.SetParent(null);
        rigB.velocity = Vector3.zero;
        rigB.angularVelocity = Vector3.zero;
        rigB.isKinematic = true;
        rigB.detectCollisions = true;
    }
}
