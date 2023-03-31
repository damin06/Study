using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged;


    public Transform BasePosition;
    public AIState CurrentState;

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeToState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform);
    }

    public void Update()
    {
        if (Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            CurrentState.UpdateState();
        }
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }
}
