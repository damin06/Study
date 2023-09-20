using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ClientSingletone : MonoBehaviour
{
    private static ClientSingletone _instance;
    public static ClientSingletone Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<ClientSingletone>(); 
            if(_instance == null)
            {
                Debug.LogError("No client singletone");
                return null;
            }
            return _instance;
        }
    }

    public ClientGameManager GameManager { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task<bool> CreateClient()
    {
        GameManager = new ClientGameManager();
        return await GameManager.InitAsync();
    }

    private void OnDestroy()
    {
        GameManager?.Dispose();
    }
}
