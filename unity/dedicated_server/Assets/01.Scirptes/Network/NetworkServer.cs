using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class UserData
{
    public string username;
}

public class NetworkServer : IDisposable
{
    public delegate void UserCahnged(ulong clientID, UserData userData);

    private NetworkObject _playerPrefab;
    private NetworkManager _networkManager;

    private Dictionary<ulong, UserData> _clientIdToUserDataDictionary = new Dictionary<ulong, UserData>(); 


    public NetworkServer(NetworkObject playerPrefab)
    {
        _playerPrefab = playerPrefab;
        _networkManager = NetworkManager.Singleton;
        _networkManager.ConnectionApprovalCallback += HandleConnectionApproval;
        _networkManager.OnServerStarted += HandleServerStarted;
    }

    private void HandleServerStarted()
    {
        _networkManager.OnClientConnectedCallback += HandleClientConnect;
        _networkManager.OnClientDisconnectCallback += HandleClientDisconnect;
    }

    private void HandleClientDisconnect(ulong clientID)
    {
        
    }

    private void HandleClientConnect(ulong clientID)
    {
        NetworkObject instance = GameObject.Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        instance.SpawnAsPlayerObject(clientID);

        UserData userData = _clientIdToUserDataDictionary[clientID];

        if(instance.TryGetComponent<Player>(out Player player))
        {
            Debug.Log($"{userData.username} is Create complete!");
            player.SetUserName(userData.username);
        }
        else
        {
            Debug.Log($"{userData.username} is Create failed!");
        }
    }

    private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest req, NetworkManager.ConnectionApprovalResponse res)
    {
        string json = Encoding.UTF8.GetString(req.Payload);
        UserData userData = JsonUtility.FromJson<UserData>(json);

        _clientIdToUserDataDictionary[req.ClientNetworkId] = userData;

        res.Approved = true;
        res.CreatePlayerObject = false;
        
        Debug.Log($"{userData.username} [ {req.ClientNetworkId} ] is logined!");
    }

    public bool OpenConnection(string ipAdress, ushort port)
    {
        UnityTransport tranport = _networkManager.GetComponent<UnityTransport>();
        tranport.SetConnectionData(ipAdress, port);
        return _networkManager.StartServer();
    }

    public void Dispose()
    {
        if(_networkManager == null) return;
        _networkManager.ConnectionApprovalCallback -= HandleConnectionApproval;
        _networkManager.OnServerStarted -= HandleServerStarted;
        _networkManager.OnClientConnectedCallback -= HandleClientConnect;
        _networkManager.OnClientDisconnectCallback -= HandleClientDisconnect;

        if (_networkManager.IsListening)
        {
            _networkManager.Shutdown();
        }
    }

    public UserData GetUserData(ulong clientID)
    {
        return _clientIdToUserDataDictionary[clientID];
    }

    [ServerRpc]
    public UserData getUserDataByClientID(ulong clientID)
    {
        if (_clientIdToUserDataDictionary.TryGetValue(clientID, out UserData userData))
        {
            return userData;
        }
        return null;
    }

}
