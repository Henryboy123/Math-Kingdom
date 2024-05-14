using Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private LevelMetadata metadata;
    [SerializeField] private Sprite lockedImage;
    [SerializeField] private List<Sprite> Stars;
    [SerializeField] private GameObject levelInfoPrefab;
    private Transform levelPanelParent;

    private void Awake()
    {
        levelPanelParent = transform;
        var playerData = LocalDB.Instance.database;
        foreach (var data in metadata.LevelImages)
        {
            var levelInfo = Instantiate(levelInfoPrefab, levelPanelParent).GetComponent<LevelInfo>();
            var levelPlayerData = playerData.LevelData.SingleOrDefault(x => x.LevelId == data.Id);
            if (levelPlayerData == null)
            {
                var newLeveldata = LevelData.Default(data.Id);
                playerData.LevelData.Add(newLeveldata);
                levelPlayerData = newLeveldata;
            }

            levelInfo.SetLevelInfo(data.Id, data.Id == 1 || playerData.LevelData[data.Id - 1].IsCompleted, data.Image, lockedImage
                , levelPlayerData.NumberOfStars != 0 ? Stars[levelPlayerData.NumberOfStars - 1] : null);
        }
    }
}
