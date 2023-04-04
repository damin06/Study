using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentInput : MonoBehaviour, IAgentInput
{

    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonPress { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonRelease { get; set; }

    private bool _fireButtonDown = false; //현재 총알발사 버튼이 눌림 상태인지

    public UnityEvent OnReloadButtonPress;

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();

        GetFireInput();
        GetReloadInput();
    }

    private void GetReloadInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            OnReloadButtonPress?.Invoke();
        }
    }

    private void GetFireInput()
    {
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            if(_fireButtonDown == false) //처음으로 버튼이 눌린거면 눌렸음을 통지
            {
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }else
        {
            if(_fireButtonDown == true)  //버튼을 땠는데 기존에 눌려있던거라면
            {
                _fireButtonDown = false;
                OnFireButtonRelease?.Invoke(); //버튼이 떼졌음을 통지
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos); //이 코드는 무조건 변경
        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector2(h, v));
    }
}
