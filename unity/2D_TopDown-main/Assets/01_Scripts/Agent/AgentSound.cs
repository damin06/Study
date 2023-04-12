using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip stepSound, hitClip, deathClip, attackSound;

    public void PlayStepSound()
    {
        PlayerClipWithVariablePitch(stepSound);
    }

    public void PlayHitClip()
    {
        PlayerClipWithVariablePitch(hitClip);
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
