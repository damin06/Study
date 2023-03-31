using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSound : AudioPlayer
{
    public AudioClip stepSound, hitClip, deathClip, attacksound;

    public void PlayStepSound()
    {
        PlayerClipWithVariablePitch(stepSound);
    }

    public void PlayeHitClip()
    {
        PlayerClipWithVariablePitch(hitClip);
    }

    public void PlayDeathClip()
    {
        PlayClip(deathClip);
    }

    public void PlayAttackClip()
    {
        PlayClip(attacksound);
    }
}
