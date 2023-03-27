using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : PoolableMono
{
    public bool IsEnemy;

    [SerializeField] private BulletDataSO _bulletData;
    private float _timeToLive;

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

        if (_timeToLive >= _bulletData.lifeTime)
        {
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            HitObstacle(collision);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }

        isDead = true;
        PoolManager.Instance.Push(this);
    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            impact.SetPositionAndRotation(hit.point + (Vector2)transform.right * 0.5f, rot);
        }
    }

    private void HitEnemy(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();


        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10f);

        if (hit.collider != null)
        {
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
            damageable?.GetHit(_bulletData.damage, gameObject, hit.point, hit.normal);
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
