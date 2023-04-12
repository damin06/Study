using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedShellGenerate : FeedBack
{
    [SerializeField] private Transform _shellPosition;

    public override void CompleteFeedBack()
    {

    }

    public override void CreateFeedBack()
    {
        Vector3 ShellPos = _shellPosition.position;
        Vector3 direction = _shellPosition.up * -1 + _shellPosition.forward * -0.5f;

        TextureParticleManager.Instance.SpawnShell(ShellPos, direction);
    }
}
