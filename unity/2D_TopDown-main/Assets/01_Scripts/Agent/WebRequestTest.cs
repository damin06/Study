using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(DownloadTexture());
        }
    }

    private IEnumerator GetJsonData()
    {
        string url = "https://ddragon.leagueoflegends.com/cdn/13.8.1/data/ko_KR/champion.json";
        UnityWebRequest req = UnityWebRequest.Get(url);

        yield return req.SendWebRequest();

        string jsonData = req.downloadHandler.text;

        LolItemJson json = JsonUtility.FromJson<LolItemJson>(jsonData);

        Debug.Log($"{json.version}, {json.type}");
    }

    private IEnumerator DownloadTexture()
    {
        string url = "http://ggm.gondr.net/image/users/223/profile/miniActionCharacter.png";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(req);

            Debug.Log(texture);
            float w = texture.width;
            float h = texture.height;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, w, h), Vector2.one * 0.5f, 128);

            gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            Debug.Log("전송실패");
            Debug.Log(req.responseCode);
        }
    }
}
