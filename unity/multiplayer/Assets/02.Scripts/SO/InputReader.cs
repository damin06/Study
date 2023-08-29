using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controlls;

[CreateAssetMenu(menuName = "SO/Input/Reader", fileName = "New Input reader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> PrimaryFireEvent;
    public event Action<Vector2> MovementEvent;
    public Vector2 AimPosition { get; private set; }
    private Controlls _controlAction;

    private void OnEnable()
    {
        if(_controlAction == null)
        {
            _controlAction = new Controlls();
            _controlAction.Player.SetCallbacks(this);
        }

        _controlAction.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementEvent?.Invoke(value);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) 
        {
            PrimaryFireEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            PrimaryFireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>(); 
    }
}
