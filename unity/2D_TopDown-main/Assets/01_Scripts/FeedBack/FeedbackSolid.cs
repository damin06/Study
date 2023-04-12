using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSolid : FeedBack
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _solidTime = 0.1f;

    public override void CompleteFeedBack()
    {
        StopAllCoroutines();
        _spriteRenderer.material.SetInt("_IsSolidColor", 0);
    }

    public override void CreateFeedBack()
    {
        if (_spriteRenderer.material.HasProperty("_IsSolidColor"))
        {
            _spriteRenderer.material.SetInt("_IsSolidColor", 1);
            StartCoroutine(WaitBeforeChangingBack());
        }
    }

    IEnumerator WaitBeforeChangingBack()
    {
        yield return new WaitForSeconds(_solidTime);
        _spriteRenderer.material.SetInt("_IsSolidColor", 0);
    }
}