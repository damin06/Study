using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClientSingnleton : MonoBehaviour
{
    public ClientGameManager GameManager { get; private set; }

    private static ClientSingnleton _instance;
    public static ClientSingnleton Instance
    {
        get
        {
            if( _instance != null ) return _instance;   
            _instance = FindAnyObjectByType<ClientSingnleton>();    
            if(_instance == null)
            {
                Debug.LogError("No client singletone");
            }
            return _instance;
        }
    }

    public void CreateClient()
    {
        GameManager = new ClientGameManager(NetworkManager.Singleton);
    }

    private void OnDestroy()
    {

    }
}
