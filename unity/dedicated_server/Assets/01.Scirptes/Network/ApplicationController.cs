using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private NetworkObject _playerPrefab;
    [SerializeField] private ServerSingleton _severPrfeab;
    [SerializeField] private ClientSingleton _clientPrefab;

    [SerializeField] private string _ipAdress;
    [SerializeField] private ushort _port;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LaunchByMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private void LaunchByMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {
            ServerSingleton server = Instantiate(_severPrfeab, transform);
            server.startServer(_playerPrefab, _ipAdress, _port);
        }
        else
        {
            ClientSingleton client = Instantiate(_clientPrefab, transform);
            client.CreateClient(_ipAdress, _port);

            SceneManager.LoadScene(SceneList.Menu);
        }
    }
}
