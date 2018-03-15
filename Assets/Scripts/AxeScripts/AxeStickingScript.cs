using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStickingScript : MonoBehaviour {
    private Rigidbody axeRB;
    private AudioSource source;
    public List<AudioClip> defaultClips;

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
        source = GetComponent<AudioSource>();
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
        if(collision.contacts[0].thisCollider.name != "AxeHead") return;
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
                
                if (bladeVelocity.magnitude > penetration.minimumSpeed)
                {
                    StickToObject(collision.gameObject, collision.contacts[0]);
                }
            }
        }
    }

    private void StickToObject(GameObject newParent, ContactPoint point)
    {
        axeRB.isKinematic = true;
        axeRB.useGravity = false;
        scoreScript = newParent.GetComponent<ScoringScript>();
        axeRB.detectCollisions = false;
        source.Stop();
        if (penetration.clips.Count > 0) {
            source.clip = penetration.clips[Mathf.FloorToInt(Random.Range(0, penetration.clips.Count))];
        }
        else
        {
            source.clip = defaultClips[Mathf.FloorToInt(Random.Range(0, defaultClips.Count))];
        }
        source.Play();
        if (newParent.transform.localScale != Vector3.one)
        {
            axeRB.transform.SetParent(newParent.transform.parent);
        }
        else
        {
            axeRB.transform.SetParent(newParent.transform);
        }
        if (scoreScript != null)
        {
            scoreScript.UpdateScore(gameObject, point);
        }
    }
}
