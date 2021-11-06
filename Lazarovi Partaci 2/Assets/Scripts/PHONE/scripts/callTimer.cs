using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class callTimer : MonoBehaviour
{
    private static callTimer instance;

    public TMP_Text text;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float timeElapsed;

    private void Awake()
    {
        instance = this;
    }

    public void beginTimer()
    {
        text.text = "00:00";
        timerGoing = true;
        timeElapsed = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void endTimer()
    {
        timerGoing = false;
        text.text = "00:00";
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            timeElapsed += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(timeElapsed);
            string timePlayingStr = timePlaying.ToString("mm':'ss");
            text.text = timePlayingStr;

            yield return null;
        }
    }
}
