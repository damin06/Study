using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Shootknife : NetworkBehaviour
{
    [Header("참조변수들")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _shootPositionTrm;
    [SerializeField] private GameObject _clientPrefab;
    [SerializeField] private GameObject _serverPrefab;
    [SerializeField] private Collider2D _playerColdier;

    [Header("셋팅값들")]
    [SerializeField] private float _knifeSpeed;
    [SerializeField] private int _knifeDamage;
    [SerializeField] private float _thriwCooltime;
    private float _lastShotTime;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        _inputReader.ShootEvent += HandleShootKnife;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
        _inputReader.ShootEvent -= HandleShootKnife;
    }

    protected void HandleShootKnife()
    {
        if (!IsOwner) return;
        if (Time.time < _lastShotTime + _thriwCooltime) return;

        _lastShotTime = Time.time;
        Vector3 pos = _shootPositionTrm.position;
        Vector3 dir = _shootPositionTrm.right;
        PrimaryFireServerRPC(pos, dir);
    }
    private void Update()
    {
        
    }

    [ServerRpc]
    private void PrimaryFireServerRPC(Vector3 pos, Vector3 dir)
    {
        UserData user = ServerSingleton.Instance.getUserDataByClientID(OwnerClientId);
        GameObject knife = Instantiate(_clientPrefab, pos, Quaternion.identity);
        knife.transform.right = dir;

        Physics2D.IgnoreCollision(_playerColdier, knife.GetComponent<Collider2D>());

        if (knife.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = dir * _knifeSpeed;
        }

        SpawnDummyProjectileClientRPC(pos, dir);
    }

    [ClientRpc]
    private void SpawnDummyProjectileClientRPC(Vector3 pos, Vector3 dir)
    {
        if(IsOwner) return;
        SpawnDummyKnife(pos, dir);
    }

    private void SpawnDummyKnife(Vector3 pos, Vector3 dir)
    {
        GameObject knife = Instantiate(_clientPrefab, pos, Quaternion.identity);
        knife.transform.right = dir;

        Physics2D.IgnoreCollision(_playerColdier, knife.GetComponent<Collider2D>());

        if (knife.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = dir * _knifeSpeed;
        }
    }
}
