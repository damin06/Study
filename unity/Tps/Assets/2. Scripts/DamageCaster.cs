using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private float casterRadius = 1f;
    [SerializeField]
    private float casterInterpolation = 0.1f;

    [SerializeField]
    private int monsterDamage = 2;

    [SerializeField]
    private LayerMask whatIsEnemy;

    public void DamageCast()
    {
        RaycastHit hit;
        bool isHit = Physics.SphereCast(transform.position - transform.forward * casterRadius, casterRadius, transform.forward, out hit, casterRadius + casterInterpolation, whatIsEnemy);
        if (isHit)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.OnDamage(monsterDamage, hit.point, hit.normal);
            }
        }
        else
        {
            Debug.Log("안맞았어요");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, casterRadius);
    }
}
