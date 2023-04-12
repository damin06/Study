using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeComtroller : MonoBehaviour
{
    public static TimeComtroller Instance;

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1.0f;
    }

    private void OnDestroy()
    {
        ResetTimeScale();
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnComplete));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnComplete)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = endTimeValue;
        OnComplete?.Invoke();
    }
}
