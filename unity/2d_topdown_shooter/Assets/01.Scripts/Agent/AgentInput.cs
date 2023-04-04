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

    private bool _fireButtonDown = false; //���� �Ѿ˹߻� ��ư�� ���� ��������

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
            if(_fireButtonDown == false) //ó������ ��ư�� �����Ÿ� �������� ����
            {
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }else
        {
            if(_fireButtonDown == true)  //��ư�� ���µ� ������ �����ִ��Ŷ��
            {
                _fireButtonDown = false;
                OnFireButtonRelease?.Invoke(); //��ư�� �������� ����
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos); //�� �ڵ�� ������ ����
        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementKeyPress?.Invoke(new Vector2(h, v));
    }
}
