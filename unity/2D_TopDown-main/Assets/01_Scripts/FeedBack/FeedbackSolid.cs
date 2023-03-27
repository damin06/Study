using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSolid : FeedBack
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _solidTime = 0.1f;

    public override void CompleteFeedBack()
    {
        StopAllCoroutines();
        _spriteRenderer.material.SetInt("_IsSolideColor", 0);
    }

    public override void CreateFeedBack()
    {
        if (_spriteRenderer.material.HasProperty("_IsSolideColor"))
        {
            _spriteRenderer.material.SetInt("_IsSolideColor", 1);
            StartCoroutine(WaitBeforeChagingBack());
        }
    }

    IEnumerator WaitBeforeChagingBack()
    {
        yield return new WaitForSeconds(_solidTime);
        _spriteRenderer.material.SetInt("_IsSolideColor", 0);
    }
}
