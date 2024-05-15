using Data;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LocalDB localDb;
    private float startTime;

    void Start()
    {
        EventsManager.Instance.OnGameStart.AddListener(StartTimer);
        EventsManager.Instance.OnWin.AddListener(onWin);
        localDb = LocalDB.Instance;
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

        localDb.SaveLevelData(levelId, true, starCount);
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }
}
