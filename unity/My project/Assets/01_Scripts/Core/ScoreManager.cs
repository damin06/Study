using Unity.Netcode;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    public NetworkVariable<GameRole> currentTurn = new NetworkVariable<GameRole>();

    public NetworkVariable<int> hostScore = new NetworkVariable<int>();  
    public NetworkVariable<int> clientScore = new NetworkVariable<int>();

    private void Start()
    {
        InitialzieScore();
    }
    
    private void InitialzieScore()
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
        if(hostScore.Value > 3)
        {

        }

        if(clientScore.Value > 3) 
        {
            
        }

        GameManager.Instance.EggManager.ResrtEgg();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        Egg.OnFallInWater += HandleFallInWater;
    }


    public override void OnNetworkDespawn()
    {
        if (!IsServer) return;
        Egg.OnFallInWater -= HandleFallInWater;
    }
}
