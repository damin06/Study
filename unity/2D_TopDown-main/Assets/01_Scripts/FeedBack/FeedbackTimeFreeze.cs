using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTimeFreeze : FeedBack
{
    [SerializeField]
    private float _freezeTimeDelay = 0.05f, _unfreezeTimeDelay = 0.02f;
    [SerializeField]
    [Range(0, 1)]
    private float _timeFreezeValue = 0.2f;

    public override void CompleteFeedBack()
    {
        TimeController.Instance?.RestTimeScale();
    }

    public override void CreateFeedBack()
    {
        TimeController.Instance?.ModifyTimeScale(_timeFreezeValue, _freezeTimeDelay, () =>
        {
            TimeController.Instance?.ModifyTimeScale(1, _unfreezeTimeDelay);
        });
    }
}
