using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button stoneBrickButton;
    [SerializeField] GameObject stoneBrickGameObject;
    [SerializeField] Button goldBrickButton;
    [SerializeField] GameObject goldBrickGameObject;
    [SerializeField] TextMeshProUGUI levelDesciption;
    [SerializeField] LevelMetadata levelMetadata;

    private void Awake()
    {
        stoneBrickButton.onClick.AddListener(() => createBrickGameObject(stoneBrickButton.GetComponent<RectTransform>(), stoneBrickGameObject));
        goldBrickButton.onClick.AddListener(() => createBrickGameObject(goldBrickButton.GetComponent<RectTransform>(), goldBrickGameObject));
        var currentLevelIndex = int.Parse(SceneManager.GetActiveScene().name.Split(' ').Last());
        levelDesciption.text = levelMetadata.LevelImages.First(x => x.Id == currentLevelIndex).LevelDescription;
    }

    public void createBrickGameObject(Transform buttonTransform, GameObject brickPrefab)
    {
        Instantiate(brickPrefab, buttonTransform.position + Vector3.right * 1.5f, Quaternion.identity);
    }
}
