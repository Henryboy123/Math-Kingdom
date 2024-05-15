using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private Image LevelImage;
    [SerializeField] private Image LockedImage;
    [SerializeField] private Image StarsImage;

    private bool IsUnlocked;
    private int LevelId;
    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene("Game " + LevelId));
    }

    public void SetLevelInfo(int levelId, bool isUnloacked, Sprite levelImage, Sprite lockedImage, Sprite starsImage)
    {
        LevelId = levelId;
        IsUnlocked = isUnloacked;
        LevelImage.sprite = levelImage;
        LockedImage.sprite = lockedImage;
        StarsImage.sprite = starsImage;

        if (starsImage == null)
        {
            StarsImage.enabled = false;
        }
        if (IsUnlocked)
        {
            LockedImage.enabled = false;
        }
        else
        {
            button.interactable = false;
        }
    }
}
