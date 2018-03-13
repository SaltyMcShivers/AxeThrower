using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningGameManager : GameManagerScript
{
    public List<SpinningTargetScript> targets;
    public float spinTime;

    public float bonusTime;

    private int currentTarget;
    private int nextTarget;

    // Use this for initialization
    void Start () {
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (!gamePlaying) return;
		if(timeKeeper.GetTime() <= 0.01f)
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
        nextTarget = Mathf.FloorToInt(Random.Range(0, targets.Count));
        targets[nextTarget].StartSpin(spinTime);
    }

    public override void ScorePoints(GameObject axe, int points, TargetScoringScript target)
    {
        if (timeKeeper.GetTime() < 0.01f) return;
        if (target.gameObject != targets[nextTarget].gameObject) return;
        if (points == 2)
        {
            timeKeeper.AddTime(bonusTime);
            targets[nextTarget].DisplayBonus();
        }
        scoreKeeper.AddToScore(1);

        currentTarget = nextTarget;
        nextTarget = Mathf.FloorToInt(Random.Range(0, targets.Count - 1));
        if (nextTarget >= currentTarget) nextTarget++;
        targets[currentTarget].StartSpin(spinTime);
        targets[nextTarget].StartSpin(spinTime);
    }

    public override void EndGame()
    {
        targets[nextTarget].StartSpin(spinTime);
        base.EndGame();
    }
}
