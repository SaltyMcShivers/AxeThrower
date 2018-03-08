using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringScript : MonoBehaviour {
    public ScoreKeeperScript scoreKeeper;

    protected int score;

    public int minimumPoints;

    public void UpdateScore(GameObject axe)
    {
        CalculateNewScore(axe);
        scoreKeeper.AddToScore(score);
    }

    protected virtual void CalculateNewScore(GameObject axe)
    {

    }
}
