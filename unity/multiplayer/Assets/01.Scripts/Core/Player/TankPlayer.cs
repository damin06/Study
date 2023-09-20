using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;
using Unity.Collections;
using System;

public class TankPlayer : NetworkBehaviour
{
    [Header("��������")][SerializeField] private CinemachineVirtualCamera _followCam;
    [field:SerializeField]public Health helathCompo { get; private set; }

    [Header("���ð�")][SerializeField] private int _ownCamPriority;
    
    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>();
    public static event Action<TankPlayer> OnPlayerSapwned;
    public static event Action<TankPlayer> OnPlayerDespawned;

    public override void OnNetworkSpawn()
    {  
        if (IsServer) //���� NetworkServer�� �����Ŵ�.
        {
            UserData user = HostSingletone.Instance.GameManager.NetworkServer.GetUserDataByClientID(OwnerClientId);
            playerName.Value = user.username;

            OnPlayerSapwned?.Invoke(this);
        }

        if (IsOwner)
        {
            _followCam.Priority = _ownCamPriority;
        }
    }

    public override void OnNetworkDespawn()
    {
        if(IsServer)
        {
            OnPlayerDespawned?.Invoke(this);    
        }
    }
}
