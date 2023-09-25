using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private TankPlayer _playerPrefab;
    [SerializeField] private float _keptCoinRatio; //죽어도 보유하고 있을 코인 비율

    //private Action<Health> DieAction = null;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return; //부활관리는 서버만 한다.

        TankPlayer[] players = FindObjectsByType<TankPlayer>(FindObjectsSortMode.None);

        foreach (var player in players)
        {
            HandlePlayerSpawned(player); //이 오브젝트가 생성되기전에 만약 플레이어가 생성되었었다면
        }

        TankPlayer.OnPlayerSpawned += HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned += HandlePlayerDeSpawned;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) return; //부활관리는 서버만 한다.
        TankPlayer.OnPlayerSpawned -= HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned -= HandlePlayerDeSpawned;
    }

    private void HandlePlayerSpawned(TankPlayer player)
    {
        player.HealthCompo.OnDie += HandlePlayerDie;
    }

    private void HandlePlayerDie(Health player)
    {
        int remainCoin = Mathf.FloorToInt(player.Tank.Coin.totalCoins.Value * _keptCoinRatio);
        if(player.Tank.Coin.totalCoins.Value <= 10)
        {
            remainCoin = 0;
        }
        //죽어서도 보존할 코인

        Destroy(player.gameObject);
        StartCoroutine(RespawnPlayer(player.OwnerClientId, remainCoin));
    }

    private IEnumerator RespawnPlayer(ulong ownerClientID, int remainCoin)
    {
        yield return null; //또는 여기서 10초 카운트다운을 먹이고 실행할 수 도있다.

        var instance = Instantiate(_playerPrefab, TankSpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        //서버에서 만들어진 플레이어를 모든 클라이언트에게 만들라고 전달하면서
        //동시에 이 플레이어가 누구의 소유인지도 알려주는거야
        instance.NetworkObject.SpawnAsPlayerObject(ownerClientID);
        instance.Coin.totalCoins.Value = remainCoin; 
    }

    private void HandlePlayerDeSpawned(TankPlayer player)
    {
        player.HealthCompo.OnDie -= HandlePlayerDie;
    }
}