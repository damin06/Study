using Cinemachine;
using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class TankPlayer : NetworkBehaviour
{
    [Header("��������")]
    [SerializeField] private SpriteRenderer _minimapIcon;
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [field:SerializeField] public Health HealthCompo { get; private set;}
    [field:SerializeField] public CoinCollector Coin { get; private set;}

    [Header("���ð�")]
    [SerializeField] private int _ownerCamPriority;
    [SerializeField] private Color _ownerColor;

    //32����Ʈ utf8���� �ѱ� => 3����Ʈ  10���� 
    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>();

    public static event Action<TankPlayer> OnPlayerSpawned;
    public static event Action<TankPlayer> OnPlayerDespawned;

    public override void OnNetworkSpawn()
    {
        SpriteRenderer sp = GameObject.Find("TankIcon").GetComponent<SpriteRenderer>(); 
        if(IsServer)  //���� NetworkServer�� �����Ŵ�.
        {
            //OwnerClientId
            //��Ʈ��ũ�������ִ� ��ųʸ��� �̿��ؼ� �� ��ũ�� �̸��� �˾Ƴ�
            //�״����� �װ� NetworkVariable�� �־��ٲ���
            UserData data = HostSingletone.Instance.GameManager.NetworkServer
                                            .GetUserDataByClientId(OwnerClientId);
            playerName.Value = data.username;

            OnPlayerSpawned?.Invoke(this);
        }

        if (IsOwner)
        {
            _minimapIcon.color = _ownerColor;
            _followCam.Priority = _ownerCamPriority;
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
