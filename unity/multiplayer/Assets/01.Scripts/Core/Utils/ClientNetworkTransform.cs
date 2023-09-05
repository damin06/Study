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

        //네트워크 매니저가 존재하고
        if(NetworkManager != null)
        {
            //연결되어있거나 서버 리스닝중이고
            if(NetworkManager.IsConnectedClient || NetworkManager.IsListening)
            {
                //전송이 가능하다면
                if(CanCommitToTransform)
                {
                    TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
                }
            }
        }
        
    }
}
