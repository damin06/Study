using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkServer : IDisposable
{
    private NetworkManager _networkManager;

    private Dictionary<ulong, string> _clientToAuthDictionary = new Dictionary<ulong, string>();
    private Dictionary<string, UserData> _authToUserDataDictionary = new Dictionary<string, UserData>();

    public NetworkServer(NetworkManager networkManager)
    {
        _networkManager = networkManager;

        _networkManager.ConnectionApprovalCallback += ApprovalCheck;

        _networkManager.OnServerStarted += OnNetworkReady;
    }

    //클라이언트들이 서버에 접속할 때 실행을 시켜줘서 요청에 따라 승인응답할 수도있고 안할수도있어
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest req, 
                                    NetworkManager.ConnectionApprovalResponse res)
    {
        UserData data = new UserData();
        data.Deserialize(req.Payload);

        _clientToAuthDictionary[req.ClientNetworkId] = data.userAuthId;
        _authToUserDataDictionary[data.userAuthId] = data;

        res.Approved = true;
        
        //이나블 되어있는 모든 위치중에 랜덤한 위치가 나온다.
        //호스트는 랜덤 포지션을 다 가져오기도 전에 들어와버려 
        var pos = TankSpawnPoint.GetRandomSpawnPos();
        res.Position = pos;
        //Vector3 dir = Vector3.zero - pos;
        //float angle = Mathf.Atan2(dir.y, dir.x) - 90f;
        res.Rotation = Quaternion.identity;
        res.CreatePlayerObject = true;
    }

    private void OnNetworkReady()
    {
        _networkManager.OnClientDisconnectCallback += OnClientDisconnect;
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if(_clientToAuthDictionary.TryGetValue(clientId, out string authID))
        {
            _clientToAuthDictionary.Remove(clientId);
            _authToUserDataDictionary.Remove(authID);
        }
    }

    public void Dispose()
    {
        if (_networkManager == null) return;

        _networkManager.ConnectionApprovalCallback -= ApprovalCheck;
        _networkManager.OnServerStarted -= OnNetworkReady;
        _networkManager.OnClientDisconnectCallback -= OnClientDisconnect;

        if(_networkManager.IsListening)
        {
            _networkManager.Shutdown();
        }
    }

    public UserData GetUserDataByClientId(ulong clientId)
    {
        if(_clientToAuthDictionary.TryGetValue(clientId, out string authId))
        {
            if(_authToUserDataDictionary.TryGetValue(authId, out UserData data))
            {
                return data;
            }
        }
        return null;
    }
}
