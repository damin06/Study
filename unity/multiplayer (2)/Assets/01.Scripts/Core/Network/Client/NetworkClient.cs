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
        _networkManager.OnClientDisconnectCallback += OnClientDisconnect;
    }

    private void OnClientDisconnect(ulong clientId)
    {
        //여기서 이제 다른 클라가 나갔을 때 처리할 일들이랑 만약 나간녀석이 호스트면 처리할거랑
        //클라이언트 아이디는 0부터 시작하고 0번은 호스트야 
        if (clientId != 0 && clientId != _networkManager.LocalClientId) return;

        Disconnect();
    }

    public void Disconnect()
    {
        if(SceneManager.GetActiveScene().name != MenuSceneName)
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        if(_networkManager.IsConnectedClient) //아직 서버에 연결수립이 되어 있는 상태라면
        {
            _networkManager.Shutdown(); //강제 연결종료
        }
    }

    public void Dispose()
    {
        if(_networkManager != null)
        {
            _networkManager.OnClientDisconnectCallback -= OnClientDisconnect;
        }
    }
}
