using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleLightFeedback : Feedback
{
    [SerializeField] private Light2D _lightTarget = null; //�۾��ϰ��� �ϴ� ����Ʈ
    [SerializeField] private float _lightOnDelay = 0.01f, _lightOffDelay = 0.01f; //Ű�� ������ ������
    [SerializeField] private bool _defaultState = false; //�⺻ ����
    
    //��� �ǵ���� �����ؾ��� ��
    public override void CompleteFeedback()
    {
        StopAllCoroutines(); //���� �� ���غ��� �����ϴ� ��� �ڷ�ƾ�� ������.
        _lightTarget.enabled = _defaultState; //�⺻���·� ����
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCoroutine(_lightOnDelay, true, () =>
        {
            StartCoroutine(ToggleLightCoroutine(_lightOffDelay, false)); 
        }));
    }

    IEnumerator ToggleLightCoroutine(float time, bool result, Action FinishCallback = null)
    {
        yield return new WaitForSeconds(time);
        _lightTarget.enabled = result;
        FinishCallback?.Invoke();
    }
}
