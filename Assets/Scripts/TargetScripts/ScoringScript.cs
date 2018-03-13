using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringScript : MonoBehaviour {
    public ScoreKeeperScript scoreKeeper;

    protected int score;

    public int minimumPoints;

    public virtual void UpdateScore(GameObject axe, ContactPoint point)
    {
        CalculateNewScore(axe, point);
        if(scoreKeeper!= null) scoreKeeper.AddToScore(score);
    }

    protected virtual void CalculateNewScore(GameObject axe, ContactPoint point)
    {

    }
}
