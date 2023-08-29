using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody is null) return;

        if(collision.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }
}
