using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimator : MonoBehaviour
{
    protected Animator _animator;
    protected readonly int _walkBoolHash = Animator.StringToHash("Walk");
    protected readonly int _deathTriggerHash = Animator.StringToHash("Death");

    public UnityEvent OnFootStep = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DeathTrigger(bool value)
    {
        if (value)
            _animator.SetTrigger(_deathTriggerHash);
        else
            _animator.ResetTrigger(_deathTriggerHash);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void SetWalkAnimation(bool value)
    {
        _animator.SetBool(_walkBoolHash, value);
    }

    public void FootStepEvent()
    {
        OnFootStep?.Invoke();
    }

    public void SetAnimationSpeed(float value)
    {
        _animator.speed = value;
    }
}
