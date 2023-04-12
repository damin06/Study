using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : AgentRenderer
{
    [SerializeField]
    private Vector3 _offset;

    private readonly int _showRateHash = Shader.PropertyToID("_ShowRate");

    private EffectScripts _effectScript;
    private AgentAnimator _animator;
    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<AgentAnimator>();
    }

    public void ShowProgress(float time, Action CallBackAction)
    {
        StartCoroutine(ShowCoroutine(time, CallBackAction));
    }

    private IEnumerator ShowCoroutine(float time, Action CallBackAction)
    {
        Material mat = _spriteRendere.material;

        _effectScript = PoolManager.Instance.Pop("DustEffect") as EffectScripts;
        _effectScript.transform.position = transform.position + _offset;
        _effectScript.PlayEffect();

        transform.localPosition = _offset;
        float currentRate = 1f;
        float percent = 0;
        float currentTime = 0;
        _animator.SetAnimationSpeed(0);
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            currentRate = Mathf.Lerp(1, -1, percent);
            mat.SetFloat(_showRateHash, currentRate);
            transform.localPosition = Vector3.Lerp(_offset, Vector3.zero, percent);
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        _animator.SetAnimationSpeed(1);
        _effectScript.stopEffect();

        CallBackAction?.Invoke();
    }

    public void Reset()
    {
        StopAllCoroutines(); //모든 코루틴을 중지시켜줘야 해.
        _animator.SetAnimationSpeed(1);
        _spriteRendere.material.SetFloat(_showRateHash, -1f);
        if (_effectScript != null)
        {
            _effectScript.stopEffect();
        }
    }
}