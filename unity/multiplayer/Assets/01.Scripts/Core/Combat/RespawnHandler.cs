using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private TankPlayer _playerPrefab;

    //private Action<Health> DieAction = null;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return; //부활관리는 서버만 한다.

        TankPlayer[] players = FindObjectsByType<TankPlayer>(FindObjectsSortMode.None);

        foreach (var player in players)
        {
            HandlePlayerSpawned(player); //이 오브젝트가 생성되기전에 만약 플레이어가 생성되었었다면
        }

        TankPlayer.OnPlayerSapwned += HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned += HandlePlayerDeSpawned;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) return; //부활관리는 서버만 한다.
        TankPlayer.OnPlayerSapwned -= HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned -= HandlePlayerDeSpawned;
    }

    private void HandlePlayerSpawned(TankPlayer player)
    {
        player.helathCompo.OnDie += HandlePlayerDie;
    }

    private void HandlePlayerDie(Health player)
    {
        Destroy(player.gameObject);
        StartCoroutine(RespawnPlayer(player.OwnerClientId));
    }

    private IEnumerator RespawnPlayer(ulong ownerClientID)
    {
        yield return null; //또는 여기서 10초 카운트다운을 먹이고 실행할 수 도있다.

        var instance = Instantiate(_playerPrefab, TankSpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        //서버에서 만들어진 플레이어를 모든 클라이언트에게 만들라고 전달하면서
        //동시에 이 플레이어가 누구의 소유인지도 알려주는거야
        instance.NetworkObject.SpawnAsPlayerObject(ownerClientID);
    }

    private void HandlePlayerDeSpawned(TankPlayer player)
    {
        player.helathCompo.OnDie -= HandlePlayerDie;
    }
}
