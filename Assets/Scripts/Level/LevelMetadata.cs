using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptables/LevelImages")]
public class LevelMetadata : ScriptableObject
{
    public List<LevelImageData> LevelImages;
}

[Serializable]
public class LevelImageData
{
    public int Id;
    public Sprite Image;
    public int StoneCount;
    public int GoldCount;
    [TextArea]
    public string LevelDescription;
}
