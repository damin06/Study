using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    //온TriggerEndter2d에서 상대방에 붙어있는 Rigidbody를 가져와야 해
    //GetComponent Health 를 가져와
    // TakeDamage( _damage) 을 부여하면 끝
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody is null) return;

        if (other.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }

}
