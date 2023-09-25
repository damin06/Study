using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : NetworkBehaviour
{
    [Header("���� ������")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _projectileSpawnTrm;
    [SerializeField] private GameObject _serverProjectilePrefab;
    [SerializeField] private GameObject _clientProjectilePrefab;
    [SerializeField] private Collider2D _playerCollider;

    [Header("���� ����")]
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _fireCooltime;

    private bool _shouldFire;
    private float _prevFireTime;

    public UnityEvent OnFire;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        _inputReader.PrimaryFireEvent += HandleFire;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
        _inputReader.PrimaryFireEvent -= HandleFire;
    }

    private void HandleFire(bool button)
    {
        _shouldFire = button;
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (!_shouldFire) return;

        if (Time.time < _prevFireTime + _fireCooltime) return; //��Ÿ���� ���� �����ִ�.

        PrimaryFireServerRPC(_projectileSpawnTrm.position, _projectileSpawnTrm.up);
        SpawnDummyProjectile(_projectileSpawnTrm.position, _projectileSpawnTrm.up);
        _prevFireTime = Time.time;
    }

    [ServerRpc] // ������ �ִ� �� ��ũ�� �� �ż��带 �����Ű�� �� RPC���̴�.
    private void PrimaryFireServerRPC(Vector3 position, Vector3 dir)
    {
        var instance = Instantiate(_serverProjectilePrefab, position, Quaternion.identity);//������ ������ �ִ°�
        instance.transform.up = dir;
        Physics2D.IgnoreCollision(_playerCollider, instance.GetComponent<Collider2D>());
        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = rigidbody.transform.up * _projectileSpeed;
        }

        SpawnDummyProjectileClientRPC(position, dir);
    }

    [ClientRpc]
    private void SpawnDummyProjectileClientRPC(Vector3 position, Vector3 dir)
    {
        if (IsOwner) return;

        SpawnDummyProjectile(position, dir);
    }

    private void SpawnDummyProjectile(Vector3 position, Vector3 dir)
    {
        var instance = Instantiate(_clientProjectilePrefab, position, Quaternion.identity);
        instance.transform.up = dir; //�̻����� �ش�������� ȸ����Ű��
        //�̰� 2���� �ö��̴� ���� �浹�� �����Ѵ�.
        Physics2D.IgnoreCollision(_playerCollider, instance.GetComponent<Collider2D>());

        OnFire?.Invoke();
        if (instance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = rigidbody.transform.up * _projectileSpeed;
        }
    }
}
