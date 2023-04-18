using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IDamagerable
{
    [SerializeField]
    private int _maxHP;
    private int _currentHP;

    public UnityEvent<int> OnInitHealth = null;
    public UnityEvent<int, int> OnHealthChanged = null;

    public int health
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
        }
    }

    [SerializeField] private bool _isDead = false;

    public bool IsEnemy => false;
    public Vector3 _hitPoint { get; set; }

    public UnityEvent OnGeHit = null;
    public UnityEvent OnDead = null;

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        health -= damage;
        OnGeHit?.Invoke();
        if (health <= 0)
        {
            OnDead?.Invoke();
            _isDead = true;
        }
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }

    public void AddHealth(int value)
    {
        health += value;
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }

    private void Start()
    {
        _currentHP = _maxHP;
        OnInitHealth?.Invoke(_maxHP);
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }
}
