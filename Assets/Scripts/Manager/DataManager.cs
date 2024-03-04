using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
#if UNITY_EDITOR
    private string path => $"{Application.dataPath}/Data";
#else
        private string path => $"{Application.persistentDataPath}/Data";
#endif

    public GameData gameData;

    private void Start()
    {
        string filePath = Path.Combine(path, "Text.txt");
        Debug.Log(File.Exists(filePath));
    }

    public void NewData()
    {
        gameData = new GameData();
        SaveData();
    }

    [ContextMenu("SaveData")]
    public void SaveData()
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string filePath = Path.Combine(path, "Text.txt");
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
    }

    [ContextMenu("LoadData")]
    public void LoadData()
    {
        string filePath = Path.Combine(path, "Text.txt");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            gameData = new GameData();
        }
    }

    public bool ExistSaveDate()
    {
        string filePath = Path.Combine(path, "Text.txt");
        Debug.Log($"Manager : {File.Exists(filePath)}");
        return File.Exists(filePath);
    }
}