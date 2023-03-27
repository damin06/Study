using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }
    public Vector3 _hitPoint { get; private set; }

    protected bool _isDead = false;

    [SerializeField]
    protected int _MaxHealth;
    protected int _currentHealth;

    public UnityEvent OngGetHit = null;
    public UnityEvent OnDie = null;

    private void Awake()
    {
        _currentHealth = _MaxHealth;
    }

    public void GetHit(int damage, GameObject DamageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        
        OngGetHit?.Invoke();
        
        if (_currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        _isDead = true;
        OnDie?.Invoke();
    }
}
