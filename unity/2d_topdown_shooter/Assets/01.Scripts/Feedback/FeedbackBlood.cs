using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackBlood : Feedback
{
    [SerializeField] private AIActionData _aiactionData;
    [SerializeField][Range(0f, 1f)] private float _sizeFactor;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        TextureParticleManager.Instance.SpawnBlood(_aiactionData.hitPoint, _aiactionData.hitNormal, _sizeFactor);
    }

}
