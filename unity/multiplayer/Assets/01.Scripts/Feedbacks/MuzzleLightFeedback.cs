using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLightFeedback : Feedback
{
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private float _turnOnTime = 0.08f;
    [SerializeField] private bool _defaultState = false;

    public override void CreateFeedback()
    {
        StartCoroutine(ActiveCoroutine());
    }

    IEnumerator ActiveCoroutine()
    {
        _muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(_turnOnTime);
        _muzzleFlash.SetActive(false);
    }

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        _muzzleFlash.SetActive(_defaultState);
    }
}
