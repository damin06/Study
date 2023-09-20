using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkClient : IDisposable
{
    private NetworkManager _networkManager;
    private const string MenuSceneName = "Menu";

    public NetworkClient(NetworkManager networkManager) 
    {
        _networkManager = networkManager;
        _networkManager.OnClientConnectedCallback += OnClientDisconnect;
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (clientId != 0 && clientId != _networkManager.LocalClientId) return;

        Disconnect();
    }

    public void Disconnect()
    {
        if(SceneManager.GetActiveScene().name != MenuSceneName) 
        {
            SceneManager.LoadScene(MenuSceneName);  
        }

        if(_networkManager.IsConnectedClient)
        {
            _networkManager.Shutdown();
        }
    }

    public void Dispose()
    {
        if(_networkManager != null) 
        {
            _networkManager.OnClientConnectedCallback -= OnClientDisconnect;
        }
    }
}
