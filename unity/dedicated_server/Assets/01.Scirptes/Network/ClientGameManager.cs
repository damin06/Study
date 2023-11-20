using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

public class ClientGameManager : IDisposable
{
    public void ConnectClient(UserData userData)
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(userData));
        NetworkManager.Singleton.StartClient();
    }

    public void Dispose()
    {
        var netManager = NetworkManager.Singleton;
        if(netManager != null && netManager.IsConnectedClient)
        {
            netManager.Shutdown();
        }
    }
}
