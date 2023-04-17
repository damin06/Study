using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;

    public UnityEvent OnAttackButtonPress = null;

    public Transform BasePosition;

    public AIState CurrentState;
    private EnemyRenderer _enemyRenederer;
    [SerializeField] private bool _isActive = false;

    private void Awake()
    {
        _enemyRenederer = transform.Find("VisualSprite").gameObject.GetComponent<EnemyRenderer>();
    }
    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ShowEnemy();
        }

        if (!_isActive) return;

        if (Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            CurrentState.UpdateState();
        }

    }

    public void ShowEnemy()
    {
        _isActive = false;
        _enemyRenederer.ShowProgress(1f, () => _isActive = true);
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public override void Reset()
    {
        _isActive = false;
    }

    public void Attack()
    {
        OnAttackButtonPress?.Invoke();
    }
}
