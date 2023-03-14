using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementkeyPress { get; set; }
    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField]
    public UnityEvent OnFiredBittonPress { get; set; }
    [field: SerializeField]
    public UnityEvent OnFireBurronRelease { get; set; }

    private bool _fireButtonDown = false;


    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
    }

    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (_fireButtonDown == false)
            {
                _fireButtonDown = true;
                OnFiredBittonPress?.Invoke();
            }
        }
        else
        {
            if (_fireButtonDown == true)
            {
                _fireButtonDown = false;
                OnFireBurronRelease?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos);
        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        OnMovementkeyPress?.Invoke(new Vector2(h, v));
    }
}
