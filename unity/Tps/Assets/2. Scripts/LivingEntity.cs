using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float initHealth = 100f;
    public float health { get; protected set; }

    public bool dead { get; protected set; }
    public Action onDeath;
    protected virtual void OnEnable()
    {

    }

    public virtual void OnDamage(float damage, Vector3 hitPostion, Vector3 hitNormal)
    {
        health -= damage;

        if (!dead && health <= 0)
            Die();
    }

    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
            return;

        health += newHealth;
    }

    public virtual void Die()
    {
        onDeath?.Invoke();
        dead = true;
    }
}
