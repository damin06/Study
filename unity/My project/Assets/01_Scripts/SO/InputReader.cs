using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Contorls.IPlayerActions
{
    public Vector2 TouchPosition { get; private set; }
    public event Action<bool> OnTouchEvent; 

    private Contorls _contols;

    private void OnEnable()
    {
        if (_contols == null)
        {
            _contols = new Contorls();
            _contols.Player.SetCallbacks(this);
        }
        _contols.Player.Enable();
    }

    public void OnMoveDelta(InputAction.CallbackContext context)
    {
        
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnTouchEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnTouchEvent?.Invoke(false);
        }
    }
}
