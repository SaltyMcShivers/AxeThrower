using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangingTargetGame : GameManagerScript
{
    public AudioSource targetAudio;

    public float blinkingTime = 1.5f;

    public List<TargetRingScript> rings;

    private List<int> targetsHit;

    // Use this for initialization
    void Start () {
        targetsHit = new List<int>();
        ResetGame();
    }

    public override void ResetGame()
    {
        targetsHit.Clear();
        foreach(TargetRingScript ring in rings)
        {
            ring.ForceOff();
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
        if (points == 0) return;
        rings[points - 1].StartBlinking();
        if (targetsHit.Contains(points)) return;
        targetsHit.Add(points);
        targetAudio.Play();


        if (targetsHit.Count == rings.Count)
        {
            EndGame();
        }
    }

    public override void EndGame()
    {
        base.EndGame();
    }
}
