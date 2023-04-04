using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackShellGenerate : Feedback
{
    [SerializeField]
    private Transform _shellPosition;

    public override void CompleteFeedback()
    {

    }

    public override void CreateFeedback()
    {
        Vector3 shellPos = _shellPosition.position;
        Vector3 direction = _shellPosition.up * -1 + _shellPosition.forward * -0.5f;

        TextureParticleManager.Instance.SpawnShell(shellPos, direction);
    }

}
