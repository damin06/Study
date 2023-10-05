using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class HealingZone : NetworkBehaviour
{
    [Header("참조값")]
    [SerializeField] private Transform _healPowerBarTrm;

    [Header("세팅값")]
    [SerializeField] private int _maxHealPower = 30;
    [SerializeField] private float _cooldown = 60f;
    [SerializeField] private float _healTickRate = 1f;
    [SerializeField] private int _coinPerTick = 5;
    [SerializeField] private int _healPerTick = 10;

    private List<TankPlayer> _playersInZone = new List<TankPlayer> ();
    private NetworkVariable<int> _healPower = new NetworkVariable<int> ();

    private float _remainCooldown;
    private float _tickTimer;

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            _healPower.OnValueChanged += HandleHealPowerCahnged;
            HandleHealPowerCahnged(0, _healPower.Value);
        }

        if (IsServer)
        {
            _healPower.Value = _maxHealPower;   
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            _healPower.OnValueChanged -= HandleHealPowerCahnged;
        }
    }

    private void HandleHealPowerCahnged(int oldPowr, int nerPower)
    {
        _healPowerBarTrm.localScale = new Vector3((float)nerPower / _maxHealPower, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        if(collision.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer tank))
        {
            Debug.Log($"Enter : {tank.playerName.Value}");
            _playersInZone.Add(tank);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!IsServer) return;
        if (collision.attachedRigidbody.TryGetComponent<TankPlayer>(out TankPlayer player))
        {
            _playersInZone.Remove(player);
            Debug.Log($"Exit : {player.playerName.Value}");
        }
    }

    private void Update()
    {
        if (!IsServer) return;

        if (_remainCooldown > 0)
        {
            _remainCooldown -= Time.deltaTime;
            if (_remainCooldown < 0)
            {
                _healPower.Value = _maxHealPower;
            }
            else
            {
                return;
            }
        }

        //여기에 왔다는건 힐파워가 존재한다. 

        _tickTimer += Time.deltaTime;
        if (_tickTimer >= _healTickRate)  //힐이 들어갈 틱이 되었어
        {
            foreach (var player in _playersInZone)
            {
                if (player.HealthCompo.currentHealth.Value == player.HealthCompo.MaxHealth) continue;
                if (player.Coin.totalCoins.Value < _coinPerTick) continue;

                player.Coin.SpendCoin(_coinPerTick);
                player.HealthCompo.RestoreHealth(_healPerTick);

                _healPower.Value --;
                if (_healPower.Value <= 0)
                {
                    ReloadClientRpc();
                    _remainCooldown = _cooldown;
                    break;
                }
            }

            _tickTimer = _tickTimer % _healTickRate;
        }
    }

    [ClientRpc]
    public void ReloadClientRpc()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = new Color(0,0,0,0);
        sp.DOFade(1, _cooldown);
    }
}
