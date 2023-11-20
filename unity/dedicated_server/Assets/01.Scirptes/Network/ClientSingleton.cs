using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ClientSingleton : MonoBehaviour
{
    private static ClientSingleton _instance;
    public static ClientSingleton Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<ClientSingleton>();

            if (_instance == null)
                Debug.LogError("Client singleton does not exits");
            return _instance;
        }
    }

    public ClientGameManager GameManager { get; private set; }
    private string _ip;
    private ushort _port;

    public void CreateClient(string ip, ushort port)
    {
        GameManager = new ClientGameManager();
        _ip = ip;
        _port = port;
    }

    private void OnDestroy()
    {
        GameManager?.Dispose();
    }

    public void startClient(UserData userData)
    {
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(_ip, _port);
        GameManager.ConnectClient(userData);
    }
}
