using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FeedbackDisolve : FeedBack
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _duration = 0.1f;

    public UnityEvent FeedbackComplete;
    public override void CompleteFeedBack()
    {
        _spriteRenderer.material.SetInt("_IsDissolve", 0);
        _spriteRenderer.material.DOComplete();
        _spriteRenderer.material.SetFloat("_Dissolve", 1);
    }

    public override void CreateFeedBack()
    {
        _spriteRenderer.material.SetInt("_IsDissolve", 1);
        _spriteRenderer.material.DOFloat(0, "_Dissolve", _duration).OnComplete(() =>
        {
            FeedbackComplete?.Invoke();
        });
    }
}
