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
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class HostGameManager : IDisposable
{
    public NetworkServer NetServer { get; private set; }

    private Allocation _allocation;
    private string _joinCode;
    private string _lobbyId;
    private const int _maxConnections = 2;

    public event Action<string, ulong> OnPlayerConnect;
    public event Action<string, ulong> OnPlayerDisconnect;

    private NetworkObject _playerPrefab;
    public HostGameManager(NetworkObject playerPrefab)
    {
        _playerPrefab = playerPrefab;
    }

    public async Task<bool> StartHostAsync(string lobbyName, UserData userData)
    {
        try
        {
            _allocation = await Relay.Instance.CreateAllocationAsync(_maxConnections);
            _joinCode = await Relay.Instance.GetJoinCodeAsync(_allocation.AllocationId);

            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            var relayServerData = new RelayServerData(_allocation, "dtls");
            transport.SetRelayServerData(relayServerData);

            NetServer = new NetworkServer(NetworkManager.Singleton, _playerPrefab);
           NetServer.OnClientJoin += HandleClientJoin;
           NetServer.OnClientLeft += HandleClientLeft;

            string userJson = JsonUtility.ToJson(userData);
                NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(userJson);


            NetworkManager.Singleton.StartHost();
            return true;
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }

    private void HandleClientLeft(string authID, ulong clientID)
    {
        OnPlayerDisconnect?.Invoke(authID, clientID);
    }

    private void HandleClientJoin(string authID, ulong clientID)
    {
        OnPlayerConnect?.Invoke(authID, clientID);
    }

    public void Dispose()
    {
        ShutdownAsync();
    }

    public async void ShutdownAsync()
    {
        NetServer.OnClientLeft -= HandleClientLeft; 
        NetServer.OnClientJoin -= HandleClientJoin;
        _lobbyId = string.Empty;
        NetServer?.Dispose();
    }
}
