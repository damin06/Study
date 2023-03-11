using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class SaveData
{
    public Vector2 PlayerPos;
}

public class SaveSystem : MonoBehaviour
{
    private string savePath;
    private string saveFileName = "/SaveFIle.txt";
    private SaveData saveData = new SaveData();
    private PlayerController player;

    private void Start()
    {
        savePath = Application.dataPath + "/SaveData/";

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);
    }

    [ContextMenu("저장")]
    public void Save()
    {
        player = FindObjectOfType<PlayerController>();
        saveData.PlayerPos = player.transform.position;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath + saveFileName, json);
    }

    [ContextMenu("로드")]
    public void Load()
    {
        if (File.Exists(savePath + saveFileName))
        {
            string json = File.ReadAllText(savePath + saveFileName);
            saveData = JsonUtility.FromJson<SaveData>(json);

            player = FindObjectOfType<PlayerController>();
            player.transform.position = saveData.PlayerPos;
        }
    }
}
