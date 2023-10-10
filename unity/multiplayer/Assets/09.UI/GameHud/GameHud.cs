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
    private NetworkList<LeaderboardEntityState> _leaderBoardEntites; //�������� ��ƼƼ�� �ִ°�

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
            //���⿡�� �̵��� �ٸ� �߰� ������ �ٲ�
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
        //�������� ��ƼƼ ����Ʈ���ٰ�
        // ���� ������ �÷��̾��� �����͸� �־����
        //�� ������ 0���� �ض�.
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
        // ��ũ�� ������� �������� �̹� �� ������Ʈ�� ������ ���� �ִ�.(������ �������� �����غ�)

        foreach(var ent in _leaderBoardEntites)
        {
            if (ent.clientID != player.OwnerClientId) continue; //�����Ǵ� �÷��̾ ã�ƾ� ��

            try
            {
                _leaderBoardEntites.Remove(ent);
            }catch(Exception e)
            {
                Debug.LogWarning($"{ent.playerName}-{ent.clientID} ������ ����");
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
                //���� ����Ʈ�� ���� �߰��� �༮�� �������� ���� ��
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
