using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {
    public List<Transform> positions;
    private LineRenderer rope;

	// Use this for initialization
	void Start () {
        rope = GetComponent<LineRenderer>();
        transform.position = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < positions.Count; i++)
        {
            rope.SetPosition(i, positions[i].position);
        }
	}
}
