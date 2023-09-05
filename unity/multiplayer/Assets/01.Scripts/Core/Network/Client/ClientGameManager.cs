using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class ClientGameManager
{
    private const string MenuScenename = "Menu";
    private JoinAllocation _allocation;
    public async Task<bool> InitAsync()
    {
        await UnityServices.InitializeAsync(); //유니티 서비스 초기화

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
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return;
        }

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        var relayServerData = new RelayServerData(_allocation, "dtls");
        transport.SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartClient();
    }
}
