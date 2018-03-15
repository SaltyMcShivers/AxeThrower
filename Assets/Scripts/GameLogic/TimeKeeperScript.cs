using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeKeeperScript : MonoBehaviour {

    public Text timeText;

    public float secondsToPass;

    public AudioClip countdown;
    public AudioClip endgame;

    public int startCountdown;
    private AudioSource source;

    private bool timing;
    private float startTime;
    private float timeLapsed;
    private float lastTime;

    private float totalTime;
    private int minutes;
    private int seconds;
    private int mSeconds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (!timing) return;
		if(secondsToPass == 0)
        {
            timeLapsed = Time.time - startTime;
        }
        else
        {
            lastTime = timeLapsed;
            timeLapsed = startTime + secondsToPass - Time.time;
            if(timeLapsed < startCountdown && timeLapsed > 0f)
            {
                if(timeLapsed % 1f > lastTime % 1f)
                {
                    if (source.clip != countdown) source.clip = countdown;
                    source.Play();
                }
            }
        }
        timeText.text = GetTime(Mathf.Max(timeLapsed, 0f));
    }

    public void StartTiming()
    {
        timing = true;
        startTime = Time.time;
        lastTime = startTime;
        source.clip = endgame;
        source.Play();
        timeLapsed = startTime;
    }

    public void StopTimer()
    {
        timing = false;
        source.clip = endgame;
        source.Play();
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
