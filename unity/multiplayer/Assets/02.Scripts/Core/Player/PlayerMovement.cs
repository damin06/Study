using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Prefer")]
    [SerializeField] private InputReader reader;
    [SerializeField] private Transform bodyTrm;
    private Rigidbody2D rigid;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turningRate = 30f;

    private Vector2 prevMovementInput;

    private void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        if (!IsOwner) return;

        if (prevMovementInput.y >= 0)
        {
            bodyTrm.transform.eulerAngles -= new Vector3(0, 0, prevMovementInput.x) * turningRate * Time.deltaTime;
        }
        else
        {
            bodyTrm.transform.eulerAngles += new Vector3(0, 0, prevMovementInput.x) * turningRate * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {

        if (!IsOwner) return;

        rigid.velocity = bodyTrm.up * (movementSpeed * prevMovementInput.y);

    }

    public override void OnNetworkSpawn()
    {

        if (!IsOwner) return;
        reader.MovementEvent += MovementHND;

    }

    public override void OnNetworkDespawn()
    {

        if (!IsOwner) return;
        reader.MovementEvent -= MovementHND;

    }

    private void MovementHND(Vector2 dir)
    {

        prevMovementInput = dir;

    }
}
