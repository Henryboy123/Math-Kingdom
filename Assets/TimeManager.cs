using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
    private float startTime;
    string timeText;
    [SerializeField] private TextMeshProUGUI timeValue;
    void Start()
    {
        EventsManager.Instance.OnGameStart.AddListener(StartTimer);
        timeText = timeValue.text;
    }

    private void Update()
    {
        float timePassed = Time.time - startTime;
        TimeSpan t = TimeSpan.FromSeconds(timePassed);
        string answer = string.Format("{0:D2}Ր:{1:D2}Վ", t.Minutes, t.Seconds);
        timeValue.text = timeText + answer;
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }
}
