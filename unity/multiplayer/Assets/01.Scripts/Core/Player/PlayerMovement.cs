using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("����������")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _bodyTrm;
    private Rigidbody2D _rigidbody;

    [Header("���ð���")]
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private float _turningRate = 30f;

    private Vector2 _prevMovementInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return; //���ʰ� �ƴ� ��쿡�� �Է°��� ���ؼ� �۵����� �������ϱ� ����
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
        //��ü�� Tread �� ���� ����

        //���� owner������ �˻��ؾ���. ���ʰ� �ƴ϶�� ������ �ʿ䰡 ���� 
        // TurningRate�ӵ���ŭ _prevMovement���� X�Է��� ȸ�� ġ�� ���ؼ� 
        // �ٵ� Ʈ�������� ȸ�������ָ� �ȴ�.
        if (!IsOwner) return;

        float zRotation = _prevMovementInput.x * -_turningRate * Time.deltaTime;
        _bodyTrm.Rotate(0, 0, zRotation);

    }

    private void FixedUpdate()
    {
        //��ġ�� �̵���ų����
        //�������� �˻��ؼ�
        // ������ٵ��� �ӵ����ٰ� �ٵ��� up�������� y���� �����ؼ� movementSpeed��ŭ �̵������ָ� �ȴ�.
        if (!IsOwner) return; //���ʰ� �ƴϸ� ����

        _rigidbody.velocity = _bodyTrm.up * (_prevMovementInput.y * _movementSpeed);

    }
}
