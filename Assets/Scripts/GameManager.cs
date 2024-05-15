using Data;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LocalDB localDb;
    private float startTime;
    // Start is called before the first frame update
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
        print(timePassed);
    }

    public void StartTimer()
    {
        startTime = Time.time;
        print(startTime);
    }
}
