using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTargetScript : TargetScoringScript
{
    
    public Animator anim;

    private bool keepSpinning;
    private bool isSpinning;

    public GameObject bonusDisplay;
    public float displayTime = 3f;

    private void Start()
    {
        isSpinning = false;
        keepSpinning = false;
        bonusDisplay.SetActive(false);
    }

    private IEnumerator SpinCoroutine(float spinTime)
    {
        isSpinning = true;
        anim.SetBool("FaceForward", !anim.GetBool("FaceForward"));
        yield return new WaitForSeconds(spinTime-0.01f);
        isSpinning = false;
        if (keepSpinning)
        {
            keepSpinning = false;
            StartCoroutine(SpinCoroutine(spinTime));
        }
    }

    public void SetSpin(bool forward, float spinTime)
    {
        if(anim.GetBool("FaceForward") != forward)
        {
            StartCoroutine(SpinCoroutine(spinTime));
        }
    }

    public void StartSpin(float spinTime)
    {
        if (isSpinning) keepSpinning = true;
        else StartCoroutine(SpinCoroutine(spinTime));
    }

    public bool Spinning()
    {
        return isSpinning;
    }

    public virtual void DisplayBonus()
    {
        StartCoroutine("BonusCoroutine");
    }

    protected virtual IEnumerator BonusCoroutine()
    {
        bonusDisplay.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        bonusDisplay.SetActive(false);
    }
}
