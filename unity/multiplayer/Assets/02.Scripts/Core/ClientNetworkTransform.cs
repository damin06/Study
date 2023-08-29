using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

    protected override void Update()
    {
        CanCommitToTransform = IsOwner;
        base.Update();

        if(NetworkManager != null)
        {
            if(NetworkManager.IsListening || NetworkManager.IsConnectedClient)
            {
                if (CanCommitToTransform)
                {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
                }
            }
        }
    }
}
