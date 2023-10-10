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
        //���⼭ ���� �ٸ� Ŭ�� ������ �� ó���� �ϵ��̶� ���� �����༮�� ȣ��Ʈ�� ó���ҰŶ�
        //Ŭ���̾�Ʈ ���̵�� 0���� �����ϰ� 0���� ȣ��Ʈ�� 
        if (clientId != 0 && clientId != _networkManager.LocalClientId) return;

        Disconnect();
    }

    public void Disconnect()
    {
        if(SceneManager.GetActiveScene().name != MenuSceneName)
        {
            SceneManager.LoadScene(MenuSceneName);
        }

        if(_networkManager.IsConnectedClient) //���� ������ ��������� �Ǿ� �ִ� ���¶��
        {
            _networkManager.Shutdown(); //���� ��������
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
