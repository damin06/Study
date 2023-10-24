using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerColorizer : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetColor(ushort idx)
    {
        SetColorClientRpc(idx);
    }

    [ClientRpc]
    private void SetColorClientRpc(ushort idx)
    {
        _spriteRenderer.color = GameManager.Instance.slimeColors[idx];
    }
}
