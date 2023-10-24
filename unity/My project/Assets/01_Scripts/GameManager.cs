using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public enum GameState
{
    Ready,
    Game,
    Win,
    Lose
}

public enum GameRole : ushort
{
    Host,
    Client
}

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public event Action<GameState> GameStateChanged;
    private GameState _gameState;

    [SerializeField] private Transform _spawnPostion;
    public Color[] slimeColors;

    public NetworkList<GameData> players;

    public GameRole myGameRole;

    private ushort _colorIndx;
    private int _readyUserCount = 0;

    private void Awake()
    {
        Instance = this;
        players = new NetworkList<GameData>();  
    }

    private void Start()
    {
        _gameState = GameState.Ready;
    }

    public override void OnNetworkSpawn()
    {
        if(IsHost)
        {
            HostSingleton.Instance.GameManager.OnPlayerConnect += OnPlayerConnectHandle;
            HostSingleton.Instance.GameManager.OnPlayerDisconnect += OnPlayerDisConnectHandle;


            var gameData = HostSingleton.Instance.GameManager.NetServer.GetUsetDataByClientID(OwnerClientId);
            OnPlayerConnectHandle(gameData.userAuthID, OwnerClientId);
            myGameRole = GameRole.Host;
        }
        else
        {
            myGameRole = GameRole.Client;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsHost)
        {
            HostSingleton.Instance.GameManager.OnPlayerConnect -= OnPlayerConnectHandle;
            HostSingleton.Instance.GameManager.OnPlayerDisconnect -= OnPlayerDisConnectHandle;
        }
    }

    private void OnPlayerConnectHandle(string authID, ulong clientID)
    {
        UserData data = HostSingleton.Instance.GameManager.NetServer.GetUsetDataByClientID(clientID);
        players.Add(new GameData
        {
            clientID = clientID,
            playerName = data.name,
            ready = false,
            colorIdx = 0,
        });
        ++_colorIndx;
    }

    private void OnPlayerDisConnectHandle(string authID, ulong clientID)
    {
        foreach(GameData data in players)
        {
            if (data.clientID != clientID) continue;
            try
            {
                players.Remove(data);
                --_colorIndx;
            }
            catch
            {
                Debug.LogError($"{data.playerName} 삭제중 오류 발생");
            }
            break;
        }
    }

    public void GameReady()
    {
        SednReadtStateServerRpc(NetworkManager.Singleton.LocalClientId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SednReadtStateServerRpc(ulong clientID)
    {
        for(int i = 0; i < players.Count; ++i) 
        {
            if (players[i].clientID != clientID) continue;

            var oldData = players[i];
            players[i] = new GameData
            {
                clientID = clientID,
                playerName = oldData.playerName,
                ready = oldData.ready,
                colorIdx = oldData.colorIdx,
            };
            _readyUserCount += players[i].ready ? 1 : -1;
            break;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        players?.Dispose(); 
    }

    public void GameStart()
    {
        if (!IsHost) return;
        if(_readyUserCount > 1)
        {
            SpawnPlayers();
            StartGameClientRpc();
        }
        else
        {
            Debug.Log("아직 플레이어들이 준비도지 않았습니다.");
        }
    }

    [ClientRpc]
    private void StartGameClientRpc()
    {
        _gameState = GameState.Game;
        GameStateChanged?.Invoke(_gameState);
    }

    private void SpawnPlayers()
    {
        foreach(var player in players)
        {
            HostSingleton.Instance.GameManager.NetServer.SpawnPlayer(player.clientID, _spawnPostion.position, player.colorIdx);
        } 
    }
}
