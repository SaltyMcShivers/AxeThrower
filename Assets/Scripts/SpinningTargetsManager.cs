using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTargetsManager : MonoBehaviour {

    public List<SpinningTargetScript> targets;
    public float stillTime;
    public float shakeTime;
    public float spinTime;

    private int currentTarget;
    private int nextTarget;

    void Start()
    {
        nextTarget = Mathf.FloorToInt(Random.Range(0, targets.Count));
        targets[nextTarget].transform.Rotate(Vector3.up * 180f);
        StartCoroutine(SpinCoroutine());
    }

    private IEnumerator SpinCoroutine()
    {
        yield return new WaitForSeconds(stillTime);
        StartShaking();
        yield return new WaitForSeconds(shakeTime);
        StartSpinning();
        yield return new WaitForSeconds(spinTime);
        StartCoroutine(SpinCoroutine());
    }

    private void StartShaking()
    {
        currentTarget = nextTarget;
        nextTarget = Mathf.FloorToInt(Random.Range(0, targets.Count - 1));
        if (nextTarget >= currentTarget) nextTarget++;
        targets[currentTarget].StartShake();
        targets[nextTarget].StartShake();
    }

    private void StartSpinning()
    {
        targets[currentTarget].StartSpin(spinTime);
        targets[nextTarget].StartSpin(spinTime);
    }
}
