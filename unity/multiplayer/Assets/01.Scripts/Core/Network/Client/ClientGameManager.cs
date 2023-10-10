using System;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;

public class ClientGameManager : IDisposable
{
    private const string MenuScenename = "Menu";
    private JoinAllocation _allocation;
    private NetworkClient _networkClient;

    public async Task<bool> InitAsync()
    {
        await UnityServices.InitializeAsync(); //유니티 서비스 초기화

        _networkClient = new NetworkClient(NetworkManager.Singleton);
        //5번 시도해서 나온 결과를 받는다.
        AuthState authState = await AuthenticationWrapper.DoAuth();

        if(authState == AuthState.Authenticated)
        {
            return true;
        }
        return false;
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene(MenuScenename);
    }

    public async Task StartClientAsync(string code)
    {
        try
        {
            _allocation = await Relay.Instance.JoinAllocationAsync(code);
        }catch(Exception e)
        {
            //UI로 잘 출력할꺼야..너네가
            Debug.LogError(e);
            return;
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        var relayServerData = new RelayServerData(_allocation, "dtls");
        transport.SetRelayServerData(relayServerData);

        //여기다가 데이터를 같이 보낸다
        UserData userData = new UserData
        {
            username = PlayerPrefs.GetString(BootstrapScreen.PlayerNameKey, "Unknown"),
            userAuthId = AuthenticationService.Instance.PlayerId
        };

        NetworkManager.Singleton.NetworkConfig.ConnectionData = userData.Serialize().ToArray();

        NetworkManager.Singleton.StartClient();
    }

    public void Dispose()
    {
        _networkClient?.Dispose();
    }

    public void Disconnect()
    {
        _networkClient.Disconnect();
    }
}
