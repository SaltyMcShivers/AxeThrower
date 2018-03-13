using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneThrowGame : GameManagerScript
{
    public List<SpinningTargetScript> targets;
    public float spinTime;

    public int timeBeforeCheck;

    private List<GameObject> axeTracker;
    private List<SpinningTargetScript> hitTargets;

    private void Awake()
    {
        axeTracker = new List<GameObject>();
        hitTargets = new List<SpinningTargetScript>();
    }

    public override void ResetGame()
    {
        base.ResetGame();
    }

    public override void StartGame()
    {
        if (gamePlaying) return;
        base.StartGame();
        hitTargets.Clear();
        foreach (SpinningTargetScript spin in targets)
        {
            spin.SetSpin(true, spinTime);
        }
        foreach(GhostAxeScript holder in holders)
        {
            holder.ResetAxe();
            holder.gameObject.SetActive(false);
        }
    }

    private SpinningTargetScript lastHit;

    public override void ScorePoints(GameObject axe, int points, TargetScoringScript target)
    {
        foreach (SpinningTargetScript spin in targets)
        {
            if (spin.gameObject == target.gameObject)
            {
                lastHit = spin;
                break;
            }
        }
        if (lastHit == null) return;
        if (hitTargets.Contains(lastHit)) return;
        lastHit.SetSpin(false, spinTime);
        hitTargets.Add(lastHit);

        if (points == 2)
        {
            axe.GetComponent<AxeResetScript>().ResetAxe();
            lastHit.DisplayBonus();
        }
        scoreKeeper.AddToScore(1);

        if(hitTargets.Count == targets.Count)
        {
            StartCoroutine(EarlyVictoryDelay());
        }
    }

    public override void EndGame()
    {
        scoreKeeper.AddToScore(axeTracker.Count);
        base.EndGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        if (!axeTracker.Contains(other.gameObject)) axeTracker.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        axeTracker.Remove(other.gameObject);
        StopCoroutine(AxeCheckDelay());
        StartCoroutine(AxeCheckDelay());
    }

    IEnumerator AxeCheckDelay()
    {
        yield return new WaitForSeconds(timeBeforeCheck);
        if(hitTargets.Count != targets.Count && axeTracker.Count == 0)
        {
            EndGame();
        }
    }

    IEnumerator EarlyVictoryDelay()
    {
        yield return new WaitForSeconds(1f);
        EndGame();
    }
}
