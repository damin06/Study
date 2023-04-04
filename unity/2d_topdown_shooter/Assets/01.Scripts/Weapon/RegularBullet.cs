using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegularBullet : PoolableMono
{
    public bool IsEnemy; //���� �Ѿ��ΰ�?
    [SerializeField]
    private BulletDataSO _bulletData;
    private float _timeToLive; //���ʵ��� ��Ƴ������ΰ�?

    private Rigidbody2D _rigid;
    private bool isDead = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;
        _rigid.MovePosition(transform.position + transform.right * _bulletData.bulletSpeed * Time.fixedDeltaTime);

        if(_timeToLive >= _bulletData.lifeTime)
        {
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        //���� ������ �Ʊ����� �������� �������� �������� üũ����� 
        //���� ���� ������ ��ֹ����� �������� üũ�ؾ����Ѵٴ°���

        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") )
        {
            HitObstacle(collision);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }

        isDead = true;
        PoolManager.Instance.Push(this);
    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f); //���� 10���� �Ѿ� ������

        if(hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot); //��͵� �ణ ����ҰŴ�.
        }
    }

    private void HitEnemy(Collider2D collision)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f, 
            1 << LayerMask.NameToLayer("Enemy")); //���� 10���� �Ѿ� ������

        if (hit.collider != null)
        {
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            damageable?.GetHit(_bulletData.damage, gameObject, hit.point, hit.normal);

            ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactEnemyPrefab.name) as ImpactScript;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            impact.SetPositionAndRotation(hit.point + randomOffset, rot);
        }

    }

    public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public override void Reset()
    {
        isDead = false;
        _timeToLive = 0;
    }
}
