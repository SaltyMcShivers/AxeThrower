using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoringScript : ScoringScript
{
    public List<TargetPointsScript> zones;

	// Use this for initialization
	override protected void CalculateNewScore(GameObject axe)
    {
        score = minimumPoints;
        foreach(TargetPointsScript zone in zones)
        {
            if (zone.CollidingWithAxe(axe))
            {
                score += zone.points;
            }
        }
    }
}
