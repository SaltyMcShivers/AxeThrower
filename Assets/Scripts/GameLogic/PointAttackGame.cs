using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttackGame : GameManagerScript {

    public List<TargetRingScript> rings;
    public List<BonusDisplay> bonuses;
    private int bonusToShow;
    
    public AudioSource targetAudio;

    // Use this for initialization
    void Start ()
    {
        ResetGame();
        bonusToShow = 0;
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
        bonuses[bonusToShow].TriggerAnim("+" + points.ToString());
        bonusToShow = 1 - bonusToShow;
        if (points == target.minimumPoints) return;
        if(targetAudio != null)
        {
            if (targetAudio.isPlaying) targetAudio.Stop();
            targetAudio.Play();
        }
        if(points > 1 + target.minimumPoints + rings.Count)
        {
            rings[rings.Count - 1].StartBlinking();
        }
        else
        {
            rings[points - target.minimumPoints - 1].StartBlinking();
        }
    }

    public override void EndGame()
    {
        base.EndGame();
    }
}
