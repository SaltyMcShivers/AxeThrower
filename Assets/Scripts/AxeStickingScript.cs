using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStickingScript : MonoBehaviour {
    public Rigidbody axeRB;
    public float minimumStickingSpeed;

    public List<BladeScript> blades;

    private Vector3 lastSpeed;
    private Vector3 speedToWorkWith;
    private Vector3 bladeVelocity;
    private Vector3 hitDirection;

    public Vector2 bladeHitLeway;

    private ScoringScript scoreScript;
    private AxePenetrationScript penetration;

    private void Start()
    {
        axeRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (axeRB.isKinematic) return;
        lastSpeed = axeRB.velocity;
    }

    private Vector3 collisionOffset;
    private Vector3 sidewaysProject;
    private float bladeAngleY;
    private Vector3 downwardProject;
    private float bladeAngleX;
    private void OnCollisionEnter(Collision collision)
    {
        speedToWorkWith = lastSpeed;
        penetration = collision.gameObject.GetComponent<AxePenetrationScript>();
        if (penetration == null) return;
        collisionOffset = collision.contacts[0].point - transform.position;
        sidewaysProject = Vector3.Project(collisionOffset, transform.right);
        bladeAngleY = Vector3.Angle(collisionOffset - sidewaysProject - transform.position, transform.up);
        if (bladeAngleY < bladeHitLeway.y || bladeAngleY > 180f - bladeHitLeway.y)
        {
            downwardProject = Vector3.Project(collisionOffset, transform.forward);
            bladeAngleX = Vector3.Angle(collisionOffset - downwardProject - transform.position, transform.up);
            if (bladeAngleX < bladeHitLeway.x || bladeAngleX > 180f - bladeHitLeway.x)
            {
                bladeVelocity = Vector3.Project(speedToWorkWith, -collision.contacts[0].normal);

                Debug.Log(axeRB.velocity.ToString() + " " + collision.contacts[0].normal.ToString() + " " + bladeVelocity.ToString());
                if (bladeVelocity.magnitude > minimumStickingSpeed)
                {
                    StickToObject(collision.gameObject);
                }
            }
        }
        /*foreach (BladeScript blade in blades)
        {
            if (blade.CollidingWithObject(collision.gameObject, collision.contacts[0]))
            {
                bladeVelocity = Vector3.Project(speedToWorkWith, -collision.contacts[0].normal);

                //Debug.Log(axeRB.velocity.ToString() + " " + collision.contacts[0].normal.ToString() + " " + bladeVelocity.ToString());
                if (bladeVelocity.magnitude > minimumStickingSpeed)
                {
                    StickToObject(collision.gameObject);
                    return;
                }
            }
        }
        */
    }

    private void StickToObject(GameObject newParent)
    {

        Debug.Log("Hit at " + bladeVelocity.magnitude.ToString());
        axeRB.isKinematic = true;
        axeRB.useGravity = false;
        scoreScript = newParent.GetComponent<ScoringScript>();
        StartCoroutine(StickFinalize(newParent));
    }

    private IEnumerator StickFinalize(GameObject go)
    {
        yield return new WaitForFixedUpdate();
        axeRB.detectCollisions = false;
        if(go.transform.localScale != Vector3.one)
        {
            axeRB.transform.SetParent(go.transform.parent);
        }
        else
        {
            axeRB.transform.SetParent(go.transform);
        }
        if (scoreScript != null)
        {
            scoreScript.UpdateScore(gameObject);
        }
    }
}
