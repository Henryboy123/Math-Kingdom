using Data;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float startTime;
    [SerializeField] private Canvas promptOnEnd;
    [SerializeField] private TextMeshProUGUI endTime;

    void Start()
    {
        EventsManager.Instance.OnGameStart.AddListener(StartTimer);
        EventsManager.Instance.OnWin.AddListener(onWin);
    }

    public void onWin()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        var levelId = int.Parse(currentSceneName.Split(' ').Last());
        int starCount = 1;
        float timePassed = Time.time - startTime;

        if (timePassed <= 180)
        {
            starCount = 3;
        }
        else if (timePassed <= 300)
        {
            starCount = 2;
        }

        TimeSpan t = TimeSpan.FromSeconds(timePassed);
        string answer = string.Format("{0:D2}Ր:{1:D2}Վ", t.Minutes, t.Seconds);
        endTime.text = endTime.text + answer;
        promptOnEnd.gameObject.SetActive(true);
        LocalDB.Instance.SaveLevelData(levelId, true, starCount);
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }
}
