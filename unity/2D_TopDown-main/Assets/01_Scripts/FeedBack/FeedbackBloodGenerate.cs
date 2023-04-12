using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBloodGenerate : FeedBack
{
    [SerializeField] private AIActionData _actionData;
    [SerializeField]
    [Range(0f, 1f)] private float _sizeBlood;

    public override void CompleteFeedBack()
    {
        
    }

    public override void CreateFeedBack()
    {
        TextureParticleManager.Instance.SpawnBlood(_actionData.hitPoint, _actionData.hitNormal, _sizeBlood);
    }
}
