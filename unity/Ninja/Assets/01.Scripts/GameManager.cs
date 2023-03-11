using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private SaveSystem _saveSystem;

    public UnityEvent<bool> OnResultData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(Instance);

        OnResultData?.Invoke(File.Exists(Application.dataPath + "/SaveData/SaveFIle.txt"));
    }

    public void ClickStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickLoad()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!operation.isDone)
            yield return null;

        _saveSystem = FindObjectOfType<SaveSystem>();
        _saveSystem.Load();
    }
}
