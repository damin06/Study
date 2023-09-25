using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Health : NetworkBehaviour
{
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    [field: SerializeField] public int MaxHealth { get; private set; } = 100;

    private bool _isDead;
    public TankPlayer Tank { get; private set; }
    public Action<Health> OnDie;
    public UnityEvent<int, int, float> OnHealthChanged; //������, ���ݰ�, ����

    private void Awake()
    {
        Tank = GetComponent<TankPlayer>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsClient)
        {
            currentHealth.OnValueChanged += HandleChangeHealth;
            HandleChangeHealth(0, MaxHealth);
        }

        if (!IsServer) return;
        currentHealth.Value = MaxHealth; //�̰� ������ 
    }

    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            currentHealth.OnValueChanged -= HandleChangeHealth;
        }
    }

    private void HandleChangeHealth(int prev, int newValue)
    {
        OnHealthChanged?.Invoke(prev, newValue, (float)newValue / MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        ModifyHealth(-damage);
    }

    public void RestoreHealth(int heal)
    {
        ModifyHealth(heal);
    }

    public void ModifyHealth(int value)
    {
        if (_isDead) return;
        currentHealth.Value = Math.Clamp(currentHealth.Value + value, 0, MaxHealth);
        if (currentHealth.Value == 0)
        {
            OnDie?.Invoke(this);
            _isDead = true;
        }
    }

}