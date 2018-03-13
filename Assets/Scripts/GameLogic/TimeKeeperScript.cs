using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeperScript : MonoBehaviour {

    public Text timeText;

    public float secondsToPass;

    private bool timing;
    private float startTime;
    private float timeLapsed;

    private float totalTime;
    private int minutes;
    private int seconds;
    private int mSeconds;
	
	// Update is called once per frame
	void Update () {
        if (!timing) return;
		if(secondsToPass == 0)
        {
            timeLapsed = Time.time - startTime;
        }
        else
        {
            timeLapsed = Mathf.Max(startTime + secondsToPass - Time.time, 0f);
        }
        timeText.text = GetTime(timeLapsed);
    }

    public void StartTiming()
    {
        timing = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        timing = false;
    }

    public float GetTime()
    {
        return timeLapsed;
    }

    public void ResetTimer()
    {
        timeText.text = GetTime(secondsToPass);
    }

    public string GetTime(float time)
    {
        string timeText;
        minutes = Mathf.FloorToInt(time / 60f);
        seconds = Mathf.FloorToInt(time % 60f);
        mSeconds = Mathf.FloorToInt((time % 1f) * 100f);
        timeText = minutes.ToString() + ":";
        if (seconds < 10) timeText += "0";
        timeText += seconds.ToString() + ":";
        if (mSeconds < 10) timeText += "0";
        timeText += mSeconds.ToString();
        return timeText;
    }

    public void AddTime(float timeToAdd)
    {
        startTime += timeToAdd;
    }
}
