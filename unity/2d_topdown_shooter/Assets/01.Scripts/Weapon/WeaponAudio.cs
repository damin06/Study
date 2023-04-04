using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    public AudioClip shootBulletClip = null, outOfBulletClip = null, reloadClip = null;

    public void PlayShootSound()
    {
        PlayClip(shootBulletClip);
    }

    public void PlayOutOfBulletSound()
    {
        PlayClip(outOfBulletClip);
    }

    public void PlayReloadSound()
    {
        PlayClip(reloadClip);
    }
}
