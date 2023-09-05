using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("참조데이터")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _bodyTrm;
    private Rigidbody2D _rigidbody;

    [Header("셋팅값들")]
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private float _turningRate = 30f;

    private Vector2 _prevMovementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return; //오너가 아닐 경우에는 입력값에 대해서 작동하지 않을꺼니까 리턴
        _inputReader.MovementEvent += HandleMovement;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return; 
        _inputReader.MovementEvent -= HandleMovement;
    }

    private void HandleMovement(Vector2 move)
    {
        _prevMovementInput = move;
    }

    private void Update()
    {
        //몸체인 Tread 를 돌릴 꺼고

        //먼저 owner인지를 검사해야해. 오너가 아니라면 실행할 필요가 없어 
        // TurningRate속도만큼 _prevMovement에서 X입력이 회전 치를 구해서 
        // 바디 트랜스폼을 회전시켜주면 된다.
        if (!IsOwner) return;

        float zRotation = _prevMovementInput.x * -_turningRate * Time.deltaTime;
        _bodyTrm.Rotate(0, 0, zRotation);

    }

    private void FixedUpdate()
    {
        //위치를 이동시킬꺼야
        //오너인지 검사해서
        // 리지드바디의 속도에다가 바디의 up방향으로 y값을 적용해서 movementSpeed만큼 이동시켜주면 된다.
        if (!IsOwner) return; //오너가 아니면 리턴

        _rigidbody.velocity = _bodyTrm.up * (_prevMovementInput.y * _movementSpeed);

    }
}
