using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTargetScript : MonoBehaviour {
    
    public Animator anim;

    private IEnumerator SpinCoroutine(float spinTime)
    {
        anim.SetTrigger("Spin");
        yield return new WaitForSeconds(spinTime-0.01f);
        transform.Rotate(Vector3.up * 180f);
    }

    public void StartShake()
    {
        anim.SetTrigger("Shake");
    }

    public void StartSpin(float spinTime)
    {
        StartCoroutine(SpinCoroutine(spinTime));
    }
}
