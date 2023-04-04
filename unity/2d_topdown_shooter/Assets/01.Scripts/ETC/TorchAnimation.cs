using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class TorchAnimation : MonoBehaviour
{
    private Light2D _light;

    private float _baseRadius;
    private float _baseIntensity;
    private int _toggle = 1;

    [SerializeField]
    private float _radiusRandomness = 1f;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _baseRadius = _light.pointLightOuterRadius;
        _baseIntensity = _light.intensity;
    }

    private void Start()
    {
        StartShake();
    }

    private void StartShake()
    {
        float targetRadius = _baseRadius + _toggle * Random.Range(0, _radiusRandomness);
        float targetIntensity = _baseIntensity + _toggle * Random.Range(0, _radiusRandomness * 0.5f);
        _toggle *= -1;

        float targetTime = Random.Range(0.5f, 0.9f);

        Sequence seq = DOTween.Sequence();

        
        var t1 = DOTween.To(() => _light.intensity,
            value => _light.intensity = value,
            targetIntensity,
            targetTime);

        var t2 = DOTween.To(
            () => _light.pointLightOuterRadius,
            value => _light.pointLightOuterRadius = value,
            targetRadius,
            targetTime);

        seq.Append(t1);
        seq.Join(t2);
        seq.AppendCallback(() => StartShake());
    }
}
