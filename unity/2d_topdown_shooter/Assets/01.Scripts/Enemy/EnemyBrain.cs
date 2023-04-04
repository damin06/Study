using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : MonoBehaviour
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged; //마우스방향전환을 

    //공격 은 어짜피 오늘 못하니까 일단 쌩까고


    public Transform BasePosition; //이게 거리측정을 몬스터의 바닥에서 

    public AIState CurrentState;

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform); //이 부분은 최적화 필요해
    }

    public void Update()
    {
        if(Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }else
        {
            CurrentState.UpdateState(); //현재 상태를 갱신한다.
        }
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }
}
