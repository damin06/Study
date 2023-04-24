using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigd;

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
        _rigd.velocity = direction;
    }

    public override void Reset()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one;
        _rigd.velocity = Vector3.zero;
    }
}
