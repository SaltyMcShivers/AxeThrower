using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRingScript : MonoBehaviour {
    public bool remainOn;
    public float blinkingTime;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("StayBlinking", remainOn);
    }

    public void StartBlinking()
    {
        StartCoroutine(RingBlink());
    }

    public void ForceOff()
    {
        StartCoroutine(ResetRing());
    }

    IEnumerator RingBlink()
    {
        anim.SetBool("Blinking", true);
        yield return new WaitForSeconds(blinkingTime);
        anim.SetBool("Blinking", false);
    }

    IEnumerator ResetRing()
    {
        anim.SetBool("StayBlinking", false);
        yield return new WaitForEndOfFrame();
        anim.SetBool("StayBlinking", remainOn);
    }

}
