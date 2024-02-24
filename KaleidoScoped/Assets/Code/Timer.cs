using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float currTime = 0f;
    public float startTime = 360f;

    [SerializeField] Text timerText;

    void Start()
    {
        currTime = startTime;
    }

    void Update()
    {
        currTime -= 1 * Time.deltaTime;
        var ts = TimeSpan.FromSeconds(currTime);
        timerText.text = string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, (int)ts.Seconds);

        if (currTime < 60)
        {
            timerText.color = Color.red;
        }

        if (currTime <= 0)
        {
            currTime = 0;
            SceneManager.LoadScene("GameOver");
        }
    }

    public bool IsTimeUp()
    {
        return currTime <= 0;
    }

}
