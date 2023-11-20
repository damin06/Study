using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;

public class Player : NetworkBehaviour
{
    [SerializeField] private TextMeshPro _nameText;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private NetworkVariable<FixedString32Bytes> _username = new NetworkVariable<FixedString32Bytes>();

    

    private void HandleNameChanged(FixedString32Bytes prev, FixedString32Bytes newValue)
    {
        _nameText.text = newValue.ToString();
    }

    public void SetUserName(string userName)
    {
        _username.Value = userName;
    }

    public override void OnNetworkSpawn()
    {
        _username.OnValueChanged += HandleNameChanged;
        HandleNameChanged("", _username.Value);

        if (IsOwner)
        {
            _virtualCamera.Priority = 15;
        }
    }
}
