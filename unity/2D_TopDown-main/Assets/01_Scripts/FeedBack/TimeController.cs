using System;
using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    public void RestTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCorutine(endTimeValue, timeToWait, OnComplete));
    }

    IEnumerator TimeScaleCorutine(float endTimeValue, float timeToWait, Action OnComplete)
    {
        yield return new WaitForSeconds(timeToWait);
        Time.timeScale = endTimeValue;
        OnComplete?.Invoke();
    }
}
