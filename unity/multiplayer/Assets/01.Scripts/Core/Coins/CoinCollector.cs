using System;
using Unity.Netcode;
using UnityEngine;

public class CoinCollector : NetworkBehaviour
{
    [Header("참조 변수")]
    [SerializeField] private BountyCoin _bountyCoinPrefab;
    [SerializeField] private Health _health;

    [Header("설정 값들")]
    [SerializeField] private float _bountyRatio = 0.8f;

    public NetworkVariable<int> totalCoins = new NetworkVariable<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Coin>(out Coin coin))
        {
            int value = coin.Collect();

            if (!IsServer) return;
            totalCoins.Value += value;
            UIManager.Instance.PopupText(value.ToString(), transform.position, Color.yellow);
        }
    }

    public override void OnNetworkSpawn()
    {
        if(!IsServer) return;
        _health.OnDie += HandleDie;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) return;
        _health.OnDie -= HandleDie;
    }

    private void HandleDie(Health health)
    {
        if(totalCoins.Value <= 10)
        {
            return;
        }

        int bountyValue = Mathf.FloorToInt(totalCoins.Value * _bountyRatio);

        float coinScale = Mathf.Clamp(bountyValue / 100.0f, 1f, 3f);
        var coinInstance = Instantiate(_bountyCoinPrefab, transform.position, Quaternion.identity);

        coinInstance.SetValue(bountyValue);
        coinInstance.NetworkObject.Spawn();

        coinInstance.setCoinToVisible(coinScale);
    }


    public void SpendCoin(int value)
    {
        totalCoins.Value -= value;
    }
}
