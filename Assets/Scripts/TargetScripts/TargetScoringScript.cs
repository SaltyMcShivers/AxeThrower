using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoringScript : ScoringScript
{
    public GameManagerScript gameManager;
    public List<Vector2> pointZones;

    private AudioSource source;
    private float distance;
    private int maxScore;

    // Use this for initialization
    protected virtual void Start()
    {
        maxScore = minimumPoints;
        foreach (Vector2 zone in pointZones)
        {
            if (distance <= zone.x)
            {
                maxScore += (int)zone.y;
            }
            else break;
        }
        source = GetComponent<AudioSource>();
    }

    override protected void CalculateNewScore(GameObject axe, ContactPoint point)
    {
        score = minimumPoints;
        distance = Vector3.Distance(transform.position, point.point);
        foreach(Vector2 zone in pointZones)
        {
            if (distance <= zone.x)
            {
                score += (int)zone.y;
            }
            else break;
        }
        if(gameManager != null) gameManager.ScorePoints(axe, score, this);
        if (source != null && score == maxScore) source.Play();
        
    }
}
