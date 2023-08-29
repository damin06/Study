using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class Health : NetworkBehaviour
{
    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    [field: SerializeField] public int Maxhealth { get; private set; } = 100;

    private bool _isDead;

    public Action<Health> OnDie;
    public UnityEvent<int, int, float> OnHealthChanged;

    public override void OnNetworkSpawn()
    {
        if(IsClient) 
        {
            currentHealth.OnValueChanged += HandleChangeHealth;
        }

        if (!IsServer) return;
        currentHealth.Value = Maxhealth;
    }

    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            currentHealth.OnValueChanged -= HandleChangeHealth;
            HandleChangeHealth(0, Maxhealth);
        }
    }

    private void HandleChangeHealth(int prev, int newValue)
    {
        OnHealthChanged?.Invoke(prev, newValue, (float)newValue / Maxhealth);
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
        if(_isDead) return;

        currentHealth.Value = Math.Clamp(currentHealth.Value + value, 0, Maxhealth);
        if (currentHealth.Value == 0)
        {
            OnHealthChanged?.Invoke(0, Maxhealth, currentHealth.Value);
             _isDead = true;
        }
        
         
    }
}
