using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip stepSound, hitClip, deathClip, attackSound;

    public void PlayStepSound()
    {
        PlayClipWithVariablePitch(stepSound);
    }

    public void PlayHitClip()
    {
        PlayClipWithVariablePitch(hitClip);
    }

    public void PlayDeathClip()
    {
        PlayClip(deathClip);
    }

    public void PlayAttackClip()
    {
        PlayClip(attackSound);
    }
}
