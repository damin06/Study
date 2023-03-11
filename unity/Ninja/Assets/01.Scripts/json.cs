using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class json : MonoBehaviour
{
    private string path;
    private string filename = "/JSON.txt";
    private SaveJSON saveJSON = new SaveJSON();
    private PlayerController player;

    void Awake()
    {
        path = Application.dataPath + "/SAVEJSON/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    // Update is called once per frame

    [ContextMenu("저장")]
    public void SAVEJSON()
    {
        player = FindObjectOfType<PlayerController>();
        saveJSON.playerPOS = player.transform.position;

        string json = JsonUtility.ToJson(saveJSON);
        File.WriteAllText(path + filename, json);
    }

    [ContextMenu("로드")]
    public void LOADJSON()
    {
        if (File.Exists(path + filename))
        {
            string json = File.ReadAllText(path + filename);
            saveJSON = JsonUtility.FromJson<SaveJSON>(json);

            player = FindObjectOfType<PlayerController>();
            player.transform.position = saveJSON.playerPOS;
        }
    }
}
[System.Serializable]
public class SaveJSON
{
    public Vector3 playerPOS;
}