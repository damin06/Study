using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class ClientGameManager : MonoBehaviour
{
    private NetworkManager _networkManager;
    private JoinAllocation _allocation;
    private bool _isLobbyRefresh = false;   

    public ClientGameManager(NetworkManager networkManager)
    {
        _networkManager = networkManager;   
    }

    public void Disconnect()
    {
        if(_networkManager.IsConnectedClient)
        {
            _networkManager.Shutdown();
        }
    }

    public async Task StartClientAsync(string joinCode, UserData userData)
    {
        try
        {
            _allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
            return;
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        var relayServerData = new RelayServerData(_allocation, "dtls");
        transport.SetRelayServerData(relayServerData);
        string json = JsonUtility.ToJson(userData);
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(json);
        NetworkManager.Singleton.StartClient();
    }
}
