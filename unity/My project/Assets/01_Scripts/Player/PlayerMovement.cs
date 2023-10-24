using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("참조값들")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("셋팅값들")]
    [SerializeField] private float _moveSpeed;


    private bool _touched;
    private Vector2 _touchStartPos;
    private Vector2 _playerStartPos;
    private float _screenWidth;

    private Rect _camRect;
    private Vector2 _spriteSize;

    private void OnEnable()
    {
        _screenWidth = Screen.width;
        Camera mainCam = Camera.main;

        float orthoSize = mainCam.orthographicSize;
        float ratio = mainCam.aspect;

        float halfWidth = orthoSize * ratio;
        float halfHeight = orthoSize;

        Vector2 topLeft = (Vector2)mainCam.transform.position + new Vector2(-halfWidth, -halfHeight);

        _camRect = new Rect(topLeft.x, topLeft.y, halfWidth * 2, halfHeight * 2);
        _spriteSize = _spriteRenderer.size;

        _inputReader.OnTouchEvent += HandleTouch;
    }

    private void OnDisable()
    {
        _inputReader.OnTouchEvent -= HandleTouch;
    }

    void Update()
    {
        if (!IsOwner) return;
        if (_touched)
        {
            HandleDrag();
        }
    }

    private void HandleDrag()
    {
        float xDiff = (_inputReader.TouchPosition - _touchStartPos).x;

        xDiff /= _screenWidth;
        xDiff *= _moveSpeed;

        float halfSprte = _spriteSize.x * 0.5f;

        float nextXPos = Mathf.Clamp(_playerStartPos.x + xDiff, _camRect.xMin + halfSprte, _camRect.xMax - halfSprte);

        transform.position = new Vector3(nextXPos, transform.position.y);
    }

    private void HandleTouch(bool value)
    {
        _touched = value;   

        if(_touched )
        {
            _touchStartPos = _inputReader.TouchPosition;
            _playerStartPos = transform.position;
        }
    }
}
