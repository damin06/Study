using Cinemachine;
using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class TankPlayer : NetworkBehaviour
{
    [Header("참조변수")]
    [SerializeField] private SpriteRenderer _minimapIcon;
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [field:SerializeField] public Health HealthCompo { get; private set;}
    [field:SerializeField] public CoinCollector Coin { get; private set;}

    [Header("셋팅값")]
    [SerializeField] private int _ownerCamPriority;
    [SerializeField] private Color _ownerColor;

    //32바이트 utf8기준 한글 => 3바이트  10글자 
    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>();

    public static event Action<TankPlayer> OnPlayerSpawned;
    public static event Action<TankPlayer> OnPlayerDespawned;

    public override void OnNetworkSpawn()
    {
        SpriteRenderer sp = GameObject.Find("TankIcon").GetComponent<SpriteRenderer>(); 
        if(IsServer)  //나는 NetworkServer가 있을거다.
        {
            //OwnerClientId
            //네트워크서버에있는 딕셔너리를 이용해서 이 탱크의 이름을 알아내
            //그다음에 그걸 NetworkVariable에 넣어줄꺼야
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
