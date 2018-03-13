using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public ScoreKeeperScript scoreKeeper;
    public TimeKeeperScript timeKeeper;

    public float resetTime;

    public List<GhostAxeScript> holders;

    protected bool gamePlaying;
    protected bool gameReseting;

    public virtual void ScorePoints(GameObject axe, int points, TargetScoringScript target)
    {

    }

    public virtual void StartGame()
    {
        ResetGame();
        if (timeKeeper != null) timeKeeper.StartTiming();
        gamePlaying = true;
    }

    public virtual void EndGame()
    {
        if (timeKeeper != null) timeKeeper.StopTimer();
        StartCoroutine("ResetTransition");
        gamePlaying = false;
    }

    public virtual void ResetGame()
    {
        if (scoreKeeper != null) scoreKeeper.ResetScore();
        if (timeKeeper != null) timeKeeper.ResetTimer();
    }

    protected virtual IEnumerator ResetTransition()
    {
        gameReseting = true;
        foreach(GhostAxeScript ghost in holders)
        {
            ghost.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(resetTime);
        foreach (GhostAxeScript ghost in holders)
        {
            ghost.gameObject.SetActive(true);
        }
        gameReseting = false;
    }
}
