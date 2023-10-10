using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Core;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private ClientSingnleton _clientPrefab;
    [SerializeField] private HostSingleton _hostPrefab;
    [SerializeField] private NetworkObject _playerPrefab;

    public static event Action<string> OnMessageEvent;

    public static ApplicationController Instance;
    private void Awake()
    {
            Instance = this;
    }

    private async void Start()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;

        OnMessageEvent?.Invoke("게임 서비스 초기화 진행중...");
        await UnityServices.InitializeAsync();

        OnMessageEvent?.Invoke("네트워크 서비스 인증중...");
        AuthenticationWrapper.OnMessageEvent += HandleAuthMessage;
        var state = await AuthenticationWrapper.DoAuth(3);

        if(state != AuthState.Authenticated)
        {
            OnMessageEvent?.Invoke("앱 인증중 오류 발생.. 앱을 다시 시작하세요.");
            return;
        }

        HostSingleton host = Instantiate(_hostPrefab, transform);
        host.CreateHost(_playerPrefab);
        ClientSingnleton client = Instantiate(_clientPrefab, transform);

        
    }

    private void HandleAuthMessage(string msg)
    {
        OnMessageEvent?.Invoke(msg);
    }

    private void OnDestroy()
    {
        AuthenticationWrapper.OnMessageEvent -= HandleAuthMessage;
    }
}
