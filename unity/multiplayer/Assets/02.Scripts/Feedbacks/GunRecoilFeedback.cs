using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GunRecoilFeedback : Feedback
{
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private float _recoilPower = 0.2f;

    public override void CreateFeedback()
    {
        float current = _targetTrm.localPosition.y;

        Sequence seq = DOTween.Sequence();
        seq.Append(_targetTrm.DOLocalMoveY(current - _recoilPower, 0.1f));
        seq.Append(_targetTrm.DOLocalMoveY(current, 0.1f));
    }

    public override void CompleteFeedback()
    {
        _targetTrm.DOComplete();
    }
}
