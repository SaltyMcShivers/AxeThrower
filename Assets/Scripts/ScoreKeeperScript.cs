using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeperScript : MonoBehaviour {

    public Text scoreText;

    protected int score;

    // Use this for initialization
    void Start () {
        score = 0;
        scoreText.text = score.ToString();
	}
	
    public void AddToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
