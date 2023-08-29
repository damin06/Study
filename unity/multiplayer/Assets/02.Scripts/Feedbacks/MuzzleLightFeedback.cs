using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleLightFeedback : Feedback
{
    [SerializeField] private GameObject _muzzleFalsh;
    [SerializeField] private float _turnOnTime = 0.08f;
    [SerializeField] private bool _defaulState = false;

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ActionCoroutine());
    }

    IEnumerator ActionCoroutine()
    {
        _muzzleFalsh.SetActive(true);
        yield return new WaitForSeconds(_turnOnTime);
        _muzzleFalsh.SetActive(false);
    }
}
