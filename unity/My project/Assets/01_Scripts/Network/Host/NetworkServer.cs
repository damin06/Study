using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class NetworkServer : IDisposable
{
    private NetworkManager _networkManager;
    public Action<string, ulong> OnClientJoin;
    public Action<string, ulong> OnServerJoin; 
    public Action<string, ulong> OnClientLeft;

    private Dictionary<ulong, string> _clientToAuthDictionary = new Dictionary<ulong, string>();
    private Dictionary<string, UserData> _authIdToUserDataDictionart = new Dictionary<string, UserData>();

    private NetworkObject _playerPrefab;

    private List<NetworkObject> _playerList = new List<NetworkObject>();

    public NetworkServer(NetworkManager networkManager, NetworkObject playerPrefab)
    {
        _networkManager = networkManager;
        _playerPrefab = playerPrefab;
        _networkManager.ConnectionApprovalCallback += ApprovalCheck;
        _networkManager.OnServerStarted += OnServerReady;
    }

    private void OnServerReady()
    {
        _networkManager.OnClientDisconnectCallback += OnClientDisconnect;
    }

    private void OnClientDisconnect(ulong clientID)
    {
        if(_clientToAuthDictionary.TryGetValue(clientID, out var authID))
        {
            _clientToAuthDictionary.Remove(clientID);   
            _authIdToUserDataDictionart.Remove(authID);
            OnClientLeft?.Invoke(authID, clientID);
        }
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest req, NetworkManager.ConnectionApprovalResponse res)
    {
        string json = Encoding.UTF8.GetString(req.Payload);
        UserData userData = JsonUtility.FromJson<UserData>(json);

        _clientToAuthDictionary[req.ClientNetworkId] = userData.userAuthID;
        _authIdToUserDataDictionart[userData.userAuthID] = userData;    

        res.Approved = true;
        res.CreatePlayerObject = false;
        OnClientJoin?.Invoke(userData.userAuthID, req.ClientNetworkId); 
    }
    
    public UserData GetUsetDataByClientID(ulong clientID)
    {
        if (_clientToAuthDictionary.TryGetValue(clientID, out string authID))
        {

            if (_authIdToUserDataDictionart.TryGetValue(authID, out UserData data))
            {
                return data;
            }
        }
        return null;
    }

    public UserData GetUserDataByAuthID(string authID)
    {
        if(_authIdToUserDataDictionart.TryGetValue(authID, out UserData data))
        {
            return data;
        }
        return null;
    }
    public void Dispose()
    {
        if (_networkManager == null) return;
        _networkManager.ConnectionApprovalCallback -= ApprovalCheck;
        _networkManager.OnServerStarted -= OnServerReady;
        _networkManager.OnClientConnectedCallback -= OnClientDisconnect;

        if (_networkManager.IsListening)
        {
            _networkManager.Shutdown();
        }
    }

    public void SpawnPlayer(ulong clientID, Vector3 position, ushort colorIdx)
    {
        var player = GameObject.Instantiate(_playerPrefab, position, Quaternion.identity);
        player.SpawnAsPlayerObject(clientID);
        _playerList.Add(player);

        PlayerColorizer color = player.GetComponent<PlayerColorizer>();
        color.SetColor(colorIdx);

        PlayerStateController controller = player.GetComponent<PlayerStateController>();
        controller.SetInitStateClientRpc(clientID == NetworkManager.Singleton.LocalClientId);
    }

    public void DestroyAllPlayer()
    {
        foreach(var p in _playerList)
        {
            GameObject.Destroy(p.gameObject);
        }
        _playerList.Clear();
    }
}
