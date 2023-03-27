using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip stepSound, hitClip, deathClip;

    public void PlayStepSound()
    {
        PlayerClipWithVariablePitch(stepSound);
    }
}
