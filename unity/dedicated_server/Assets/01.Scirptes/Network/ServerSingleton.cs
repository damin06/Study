using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerSingleton : MonoBehaviour
{
    private static ServerSingleton _instance;
    public static ServerSingleton Instance
    {
        get
        {
            if(_instance != null) return _instance;
            _instance = FindObjectOfType<ServerSingleton>();

            if (_instance == null)
                Debug.LogError("Server singleton does not exits");

            return _instance;
        }
        
    }

    public NetworkServer NetServer { get; private set; }

    public void startServer(NetworkObject playerPrefab, string ipAdress, ushort port)
    {
        NetServer = new NetworkServer(playerPrefab);

        if (NetServer.OpenConnection(ipAdress, port))
        {
            NetworkManager.Singleton.SceneManager.LoadScene(SceneList.Game, LoadSceneMode.Single);
            Debug.Log($"{ipAdress} : {port.ToString()} : Server launching!");
        }
        else
        {
            Debug.LogError($"{ipAdress} : {port.ToString()} : Server launching failed!");
        }
    }

    public void startClient(UserData userData)
    {

    }

    private void OnDestroy()
    {
        NetServer?.Dispose();
    }

    public UserData getUserDataByClientID(ulong ownerClientId)
    {
        return NetServer.getUserDataByClientID(ownerClientId);
    }
}
