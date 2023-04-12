using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTimeFreeze : FeedBack
{
    [SerializeField] private float _freezeTimeDelay = 0.05f, _unFreezeTimeDelay = 0.02f;
    [SerializeField][Range(0,1f)] private float _timeFreezeValue = 0.2f;
    public override void CompleteFeedBack()
    {
        if(TimeComtroller.Instance != null)
        TimeComtroller.Instance?.ResetTimeScale();
    }

    public override void CreateFeedBack()
    {
        TimeComtroller.Instance?.ModifyTimeScale(_timeFreezeValue, _freezeTimeDelay, () =>
        {
            TimeComtroller.Instance?.ModifyTimeScale(1, _unFreezeTimeDelay);
        });
    }
}
