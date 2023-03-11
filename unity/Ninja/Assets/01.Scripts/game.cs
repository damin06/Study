using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    private game gm;
    public UnityEvent<bool> onuser;
    private json js;
    void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gm);
        }

        onuser?.Invoke(File.Exists(Application.dataPath + "/SAVEJSON/JSON.txt"));
    }

    // Update is called once per frame
    IEnumerator loadSceen()
    {
        AsyncOperation asaasada = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        if (!asaasada.isDone)
        {
            yield return null;
        }

        js = FindObjectOfType<json>();
        js.LOADJSON();
    }
}
