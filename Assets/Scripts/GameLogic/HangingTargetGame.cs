using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangingTargetGame : GameManagerScript
{
    public Text scoreText;
    public int maxTargets;

    private List<int> targetsHit;

    // Use this for initialization
    void Start () {
        targetsHit = new List<int>();
        ResetGame();
    }

    public override void ResetGame()
    {
        targetsHit.Clear();
        scoreText.text = "";
        for (int i = 0; i < maxTargets; i++)
        {
            scoreText.text += (i + 1).ToString();
        }
        base.ResetGame();
    }

    public override void StartGame()
    {
        if (gamePlaying) return;
        base.StartGame();
    }

    public override void ScorePoints(GameObject axe, int points, TargetScoringScript target)
    {
        Debug.Log("Scored");
        if (points == 0) return;
        if (targetsHit.Contains(points)) return;
        targetsHit.Add(points);
        scoreText.text = scoreText.text.Replace(points.ToString(), "");
        if (targetsHit.Count == maxTargets)
        {
            EndGame();
        }
    }

    public override void EndGame()
    {
        base.EndGame();
    }
}
