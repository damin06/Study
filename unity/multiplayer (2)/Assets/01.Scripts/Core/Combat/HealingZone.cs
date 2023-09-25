using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

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
            Debug.Log(tank.transform.name);
            _playersInZone.Add(tank);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void Update()
    {
        
    }
}
