using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class GameHud : NetworkBehaviour
{
    [SerializeField] private VisualTreeAsset _boardItemAsset;
    [SerializeField] private int _displayCount = 7;
    [SerializeField] private Color _ownerColor;

    private Leaderboard _leaderboard;
    private NetworkList<LeaderboardEntityState> _leaderBoardEntites; //리더보드 엔티티를 넣는곳

    private UIDocument _document;
    private void Awake()
    {
        _leaderBoardEntites = new NetworkList<LeaderboardEntityState>();
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _document.rootVisualElement;
        var boardContainer = root.Q<VisualElement>("leaderboard");
        _leaderboard = new Leaderboard(boardContainer, _boardItemAsset, _ownerColor, _displayCount);
    }

    public override void OnNetworkSpawn()
    {
        if(IsClient)
        {
            _leaderBoardEntites.OnListChanged += HandleLeaderboardChanged;
            //여기에는 이따가 다른 추가 과제를 줄께
            foreach(var ent in _leaderBoardEntites)
            {
                HandleLeaderboardChanged(new NetworkListEvent<LeaderboardEntityState>
                {
                    Type = NetworkListEvent<LeaderboardEntityState>.EventType.Add,
                    Value = ent,
                });
            }
        }

        if(IsServer)
        {
            TankPlayer[] players = FindObjectsByType<TankPlayer>(FindObjectsSortMode.None);
            foreach(TankPlayer player in players)
            {
                HandlePlayerSpawned(player);
            }

            TankPlayer.OnPlayerSpawned += HandlePlayerSpawned;
            TankPlayer.OnPlayerDespawned += HandlePlayerDeSpawned;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsClient)
        {
            _leaderBoardEntites.OnListChanged -= HandleLeaderboardChanged;
        }
        if (IsServer)
        {
            TankPlayer.OnPlayerSpawned -= HandlePlayerSpawned;
            TankPlayer.OnPlayerDespawned -= HandlePlayerDeSpawned;
        }
    }

    private void HandlePlayerSpawned(TankPlayer player)
    {
        //리더보드 엔티티 리스트에다가
        // 지금 접속한 플레이어의 데이터를 넣어줘라
        //단 코인은 0으로 해라.
        _leaderBoardEntites.Add(new LeaderboardEntityState
        {
            clientID = player.OwnerClientId,
            playerName = player.playerName.Value,
            coins = 0
        });

        player.Coin.totalCoins.OnValueChanged += (oldCoin, newCoin) =>
        {
            HandleCoinsChanged(player.OwnerClientId, newCoin);
        };
    }

    private void HandleCoinsChanged(ulong ownerClientId, int newCoin)
    {
        for(int i = 0; i < _leaderBoardEntites.Count; ++i)
        {
            if (_leaderBoardEntites[i].clientID != ownerClientId) continue;

            var oldItem = _leaderBoardEntites[i];
            _leaderBoardEntites[i] = new LeaderboardEntityState
            {
                clientID = oldItem.clientID,
                playerName = oldItem.playerName,
                coins = newCoin
            };
            break;
        }
    }

    private void HandlePlayerDeSpawned(TankPlayer player)
    {
        if (_leaderBoardEntites == null) return;
        // 탱크가 사라지는 시점에서 이미 이 오브젝트가 날라갔을 수도 있다.(게임이 꺼질때를 생각해봐)

        foreach(var ent in _leaderBoardEntites)
        {
            if (ent.clientID != player.OwnerClientId) continue; //디스폰되는 플레이어를 찾아야 해

            try
            {
                _leaderBoardEntites.Remove(ent);
            }catch(Exception e)
            {
                Debug.LogWarning($"{ent.playerName}-{ent.clientID} 삭제중 오류");
            }
            break;
        }
        player.Coin.totalCoins.OnValueChanged = null;
    }

    private void HandleLeaderboardChanged(NetworkListEvent<LeaderboardEntityState> evt)
    {
        switch (evt.Type)
        {
            case NetworkListEvent<LeaderboardEntityState>.EventType.Add:
                //현재 리스트에 지금 추가된 녀석이 존재하지 않을 때
                if(!_leaderboard.CheckExistByClientID(evt.Value.clientID))
                {
                    _leaderboard.AddItem(evt.Value);
                }
                break;
            case NetworkListEvent<LeaderboardEntityState>.EventType.Remove:
                _leaderboard.RemoveByClientID(evt.Value.clientID);
                break;
            case NetworkListEvent<LeaderboardEntityState>.EventType.Value:
                _leaderboard.UpdateByClientID(evt.Value.clientID, evt.Value.coins);
                break;
        }

        _leaderboard.SortOrder();
    }
}
