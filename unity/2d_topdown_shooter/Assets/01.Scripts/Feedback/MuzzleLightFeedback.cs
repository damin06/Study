using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleLightFeedback : Feedback
{
    [SerializeField] private Light2D _lightTarget = null; //작업하고자 하는 라이트
    [SerializeField] private float _lightOnDelay = 0.01f, _lightOffDelay = 0.01f; //키고 꺼짐의 딜레이
    [SerializeField] private bool _defaultState = false; //기본 상태
    
    //모든 피드백을 종료해야할 때
    public override void CompleteFeedback()
    {
        StopAllCoroutines(); //지금 이 비해비어에서 동작하는 모든 코루틴을 종료함.
        _lightTarget.enabled = _defaultState; //기본상태로 변경
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
