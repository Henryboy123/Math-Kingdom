using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Data
{
    public class LocalDB : MonoBehaviourSingleton<LocalDB>
    {
        internal PlayerData database;
        private readonly string fileName = "gfxscof.sffod";
        private string path;

        protected override void Awake()
        {
            base.Awake();
#if UNITY_ANDROID && !UNITY_EDITOR || UNITY_IOS && !UNITY_EDITOR
            path = global::System.IO.Path.Combine(Application.persistentDataPath, fileName);
#else
            path = global::System.IO.Path.Combine(Application.dataPath, fileName);
#endif
            DontDestroyOnLoad(this);
            Load();
        }

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            Save();
        }
#endif

        public void Load()
        {
            var json = ReadFromFIle();
            if (string.IsNullOrEmpty(json)) return;
            database = JsonUtility.FromJson<PlayerData>(json);
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(database);
            WriteToFile(json);
        }

        public void SaveLevelData(int levelId, bool isCompleted, int numberOfStarts)
        {
            print($"Saving new data {levelId}, {isCompleted}, {numberOfStarts}");
            var existingLeveldata = database.LevelData.FirstOrDefault(x => x.LevelId == levelId);
            if (existingLeveldata == null)
            {
                existingLeveldata = LevelData.Default(levelId);
                database.LevelData.Add(existingLeveldata);
            }
            if (existingLeveldata.NumberOfStars < numberOfStarts)
            {
                existingLeveldata.NumberOfStars = numberOfStarts;
            }
            existingLeveldata.IsCompleted = isCompleted;
        }

        private void WriteToFile(string json)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using StreamWriter writer = new StreamWriter(fileStream);
            writer.Write(json);
        }

        private string ReadFromFIle()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                database = new PlayerData();
                Save();
            }
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            return json;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int CoinAmount { get; set; }

        public List<LevelData> LevelData { get; set; }

        public PlayerData()
        {
            LevelData = new List<LevelData>();
        }
    }

    public class LevelData
    {
        public int LevelId { get; set; }

        public bool IsCompleted { get; set; }

        public int NumberOfStars { get; set; }

        public static LevelData Default(int levelId)
        {
            return new LevelData()
            {
                LevelId = levelId,
                IsCompleted = false,
                NumberOfStars = 0,
            };
        }
    }
}