using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigd;
    private bool _isDead = false;

    [SerializeField] private LayerMask _WhatIsEnemy;
    [SerializeField] private BulletDataSO _bulletData;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigd = GetComponent<Rigidbody2D>();
    }

    public void Flip(bool value)
    {
        _spriteRenderer.flipX = value;
    }

    public void Fire(Vector2 direction)
    {
        _rigd.velocity = direction * _bulletData.bulletSpeed;
    }

    public override void Reset()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one;
        _rigd.velocity = Vector3.zero;
        _isDead = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) ||
                   ((1 << collision.gameObject.layer) & _WhatIsEnemy) > 0)
        {
            HitObstacle(collision);

            _isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;

        Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, 2.5f, _WhatIsEnemy);

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        Vector3 explosionPosition = transform.position + transform.right * 0.5f;

        impact.SetPositionAndRotation(explosionPosition, rot);

        foreach (Collider2D colider in coliders)
        {
            if (colider.TryGetComponent(out IDamagerable health))
            {
                Vector3 normal = (transform.position - colider.transform.position).normalized;
                health.GetHit(_bulletData.damage, gameObject, colider.transform.position, normal);
            }
        }
    }
}
