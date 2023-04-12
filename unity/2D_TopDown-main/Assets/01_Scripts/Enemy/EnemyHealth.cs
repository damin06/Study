using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamagerable
{
    public bool IsEnemy { get; set; }

    public Vector3 _hitPoint { get; private set; }

    protected bool _isDead = false;

    [SerializeField] protected int _maxHealth;

    protected int _currentHealth;

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDie = null;

    private AIActionData _actionData;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        _actionData.hitNormal = normal;
        _actionData.hitPoint = hitPoint;

        _currentHealth -= damage;

        OnGetHit?.Invoke();

        if (_currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        OnDie?.Invoke();
    }
}
