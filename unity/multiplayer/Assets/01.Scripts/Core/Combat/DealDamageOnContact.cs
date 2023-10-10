using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnContact : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    //��TriggerEndter2d���� ���濡 �پ��ִ� Rigidbody�� �����;� ��
    //GetComponent Health �� ������
    // TakeDamage( _damage) �� �ο��ϸ� ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody is null) return;

        if (other.attachedRigidbody.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
        }
    }

}
