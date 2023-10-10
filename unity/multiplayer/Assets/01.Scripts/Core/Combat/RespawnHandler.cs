using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private TankPlayer _playerPrefab;
    [SerializeField] private float _keptCoinRatio; //�׾ �����ϰ� ���� ���� ����

    //private Action<Health> DieAction = null;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return; //��Ȱ������ ������ �Ѵ�.

        TankPlayer[] players = FindObjectsByType<TankPlayer>(FindObjectsSortMode.None);

        foreach (var player in players)
        {
            HandlePlayerSpawned(player); //�� ������Ʈ�� �����Ǳ����� ���� �÷��̾ �����Ǿ����ٸ�
        }

        TankPlayer.OnPlayerSpawned += HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned += HandlePlayerDeSpawned;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) return; //��Ȱ������ ������ �Ѵ�.
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
        //�׾�� ������ ����

        Destroy(player.gameObject);
        StartCoroutine(RespawnPlayer(player.OwnerClientId, remainCoin));
    }

    private IEnumerator RespawnPlayer(ulong ownerClientID, int remainCoin)
    {
        yield return null; //�Ǵ� ���⼭ 10�� ī��Ʈ�ٿ��� ���̰� ������ �� ���ִ�.

        var instance = Instantiate(_playerPrefab, TankSpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        //�������� ������� �÷��̾ ��� Ŭ���̾�Ʈ���� ������ �����ϸ鼭
        //���ÿ� �� �÷��̾ ������ ���������� �˷��ִ°ž�
        instance.NetworkObject.SpawnAsPlayerObject(ownerClientID);
        instance.Coin.totalCoins.Value = remainCoin; 
    }

    private void HandlePlayerDeSpawned(TankPlayer player)
    {
        player.HealthCompo.OnDie -= HandlePlayerDie;
    }
}