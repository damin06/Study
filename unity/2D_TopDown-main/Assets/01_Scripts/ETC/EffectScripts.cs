using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectScripts : PoolableMono
{
    [SerializeField]
    private float _stopTime = 0.5f, _lightOffTime = 1f;
    private ParticleSystem _particleEffect;
    private Light2D _light;
    private float _initIntensity;

    private void Awake()
    {
        _particleEffect = GetComponent<ParticleSystem>();
        _light = transform.Find("Light2D").GetComponent<Light2D>();
        _initIntensity = _light.intensity;
        _light.enabled = false;
    }

    public void PlayEffect(float time)
    {
        _particleEffect.Play();
        _light.enabled = true;
        StartCoroutine(StopDelay(time));
    }

    private IEnumerator StopDelay(float time)
    {
        yield return new WaitForSeconds(time);
        stopEffect();
    }

    public void stopEffect()
    {
        StartCoroutine(Delayoff());
    }

    private IEnumerator Delayoff()
    {
        float currentTime = 0f;
        float remainTime = _lightOffTime;
        bool isStop = false;
        while (currentTime < _lightOffTime)
        {
            currentTime += Time.deltaTime;
            remainTime = _lightOffTime - currentTime;
            if (remainTime < _stopTime && isStop == false)
            {
                _particleEffect.Stop();
                isStop = true;
            }
            _light.intensity = Mathf.Lerp(_initIntensity, 0, currentTime / _lightOffTime);
            yield return null;
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {
        _light.intensity = _initIntensity;
    }
}

