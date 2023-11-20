using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _movementSpeed;
    private PlayerAnimation _playerAnimation;


    private Vector2 _movermentInput;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _playerAnimation = transform.Find("Visual").GetComponent<PlayerAnimation>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        _inputReader.MovementEvent += OnMoveInput;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
        _inputReader.MovementEvent -= OnMoveInput;
    }

    private void OnMoveInput(Vector2 input)
    {
        _movermentInput = input;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        Move();
    }

    private void Move()
    {
        _playerAnimation.SetMove(_rigidbody2D.velocity.magnitude > 0.1f);

        _playerAnimation.FlipController(_rigidbody2D.velocity.x);
        _rigidbody2D.velocity = _movermentInput * _movementSpeed;
    }
}
