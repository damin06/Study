using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFeedback : FeedBack
{
    [SerializeField] private Transform _objectToShake;
    [SerializeField] private float _duration = 0.2f, _strenth = 1f, _randomness = 90f;
    [SerializeField] private int _vibrato = 10;

    [SerializeField] private bool _snapping = false, _fadOut = true;

    public override void CompleteFeedback()
    {
        _objectToShake.DOComplete();
    }

    public override void CreatFeedBack()
    {
        CompleteFeedback();
        _objectToShake.DOShakePosition(_duration, _strenth, _vibrato, _randomness, _snapping);
    }
}
