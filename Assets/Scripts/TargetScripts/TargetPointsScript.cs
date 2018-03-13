using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointsScript : MonoBehaviour {

    public int points;

    private List<GameObject> axes;

    private void Start()
    {
        axes = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " enter " + axes.Count.ToString());
        if (!axes.Contains(other.transform.parent.gameObject))
        {
            axes.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(gameObject.name + " exit " + axes.Count.ToString());
        axes.Remove(other.transform.parent.gameObject);
    }

    public void RemoveAxe(GameObject axe)
    {
        axes.Remove(axe);
    }

    public bool CollidingWithAxe(GameObject axe)
    {
        return axes.Contains(axe);
    }
}
