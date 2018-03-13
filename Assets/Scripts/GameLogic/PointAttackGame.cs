using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttackGame : GameManagerScript {

	// Use this for initialization
	void Start ()
    {
        ResetGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!gamePlaying) return;
        if (timeKeeper.GetTime() <= 0.01f)
        {
            EndGame();
        }
    }

    public override void ResetGame()
    {
        base.ResetGame();
    }

    public override void StartGame()
    {
        if (gamePlaying) return;
        base.StartGame();
    }

    public override void ScorePoints(GameObject axe, int points, TargetScoringScript target)
    {
        scoreKeeper.AddToScore(points);
    }

    public override void EndGame()
    {
        base.EndGame();
    }
}
