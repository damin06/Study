using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ScoreManager : NetworkBehaviour
{
    public NetworkVariable<GameRole> currentTurn = new NetworkVariable<GameRole>();

    public NetworkVariable<int> hostScore = new NetworkVariable<int>();  
    public NetworkVariable<int> clientScore = new NetworkVariable<int>();


    private void HandleScoreChanged(int oldScore, int newScore)
    {
        SignalHub.OnScoreChanged(hostScore.Value, clientScore.Value);
    }

    private void Start()
    {
        InitialzieScore();
    }
    
    public void InitialzieScore()
    {
        hostScore.Value = 0;
        clientScore.Value = 0;
    }

    private void HandleFallInWater()
    {
        switch (GameManager.Instance.TurnManager.currentTurn.Value)
        {
            case GameRole.Host:
                clientScore.Value += 1;
                break;
            case GameRole.Client:
                hostScore.Value += 1;
                break;
        }

        CheckForEndGame();
    }

    private void CheckForEndGame()
    {
        if(hostScore.Value >= 3)
        {
            GameManager.Instance.SendResultToClient(GameRole.Host);
        }
        else if(clientScore.Value >= 3) 
        {
            GameManager.Instance.SendResultToClient(GameRole.Host);
        }
        else
        {
            GameManager.Instance.EggManager.ResrtEgg();
        }
    }

    public override void OnNetworkSpawn()
    {
        hostScore.OnValueChanged += HandleScoreChanged; 
        clientScore.OnValueChanged += HandleScoreChanged;

        if (!IsServer) return;
        Egg.OnFallInWater += HandleFallInWater;
    }


    public override void OnNetworkDespawn()
    {
        hostScore.OnValueChanged -= HandleScoreChanged;
        clientScore.OnValueChanged -= HandleScoreChanged;

        if (!IsServer) return;
        Egg.OnFallInWater -= HandleFallInWater;
    }
}
