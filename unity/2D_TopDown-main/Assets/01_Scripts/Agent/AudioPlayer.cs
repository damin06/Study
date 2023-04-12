using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    protected AudioSource _audioSource;
    [SerializeField] protected float _pitchRandomness = 0.2f;
    protected float _basePitch;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _basePitch = _audioSource.pitch;
    }

    public void PlayerClipWithVariablePitch(AudioClip clip)
    {
        float randomPitch = Random.Range(-_pitchRandomness, _pitchRandomness);
        _audioSource.pitch = _basePitch + randomPitch;
        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
