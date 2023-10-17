using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        OnMessageEvent?.Invoke("���� ���� �ʱ�ȭ ������...");
        await UnityServices.InitializeAsync();

        OnMessageEvent?.Invoke("��Ʈ��ũ ���� ������...");
        AuthenticationWrapper.OnMessageEvent += HandleAuthMessage;
        var state = await AuthenticationWrapper.DoAuth(3);

        if(state != AuthState.NotAuthenticated)
        {
            OnMessageEvent?.Invoke("�� ������ ���� �߻�.. ���� �ٽ� �����ϼ���.");
            return;
        }

        HostSingleton host = Instantiate(_hostPrefab, transform);
        host.CreateHost(_playerPrefab);

        ClientSingnleton client = Instantiate(_clientPrefab, transform);
        client.CreateClient();

        SceneManager.LoadScene(SceneList.MenuScene);
    }

    private void HandleAuthMessage(string msg)
    {
        OnMessageEvent?.Invoke(msg);
    }

    private void OnDestroy()
    {
        AuthenticationWrapper.OnMessageEvent -= HandleAuthMessage;
    }

    public async Task<bool> StartHost(string username, string lobbyName)
    {
        var userData = new UserData
        {
            name = username,
            userAuthID = AuthenticationService.Instance.PlayerId
        };

        return await HostSingleton.Instance.GameManager.StartHostAsync(lobbyName, userData);
    }

    public async Task<List<Lobby>> GetLobbyList()
    {
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;
            options.Filters = new List<QueryFilter>()
            {
                new QueryFilter(field: QueryFilter.FieldOptions.AvailableSlots,
                op: QueryFilter.OpOptions.GT,
                value: "0"),
                new QueryFilter(field: QueryFilter.FieldOptions.IsLocked,
                op: QueryFilter.OpOptions.EQ,
                value: "0"),
            };
            QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);
            return lobbies.Results;
        }catch (LobbyServiceException ex)
        {
            Debug.LogError(ex);
            return new List<Lobby>();
        }
    }
}
