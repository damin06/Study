using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class GameHUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _startGameBtn;
    private Button _readyGameBtn;

    private List<PlayerUI> _players = new();
    private int _currentPlayer;

    private VisualElement _container;

    public void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();   
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;
        _startGameBtn = root.Q<Button>("btn-start");
        _readyGameBtn = root.Q<Button>("btn-ready");
        _container = root.Q<VisualElement>("container");
        root.Query<VisualElement>(className: "player").ToList().ForEach(x =>
        {
            var player = new PlayerUI(x);
            _players.Add(player);   
            player.RemovePlayerUI();
        });

        _startGameBtn.RegisterCallback<ClickEvent>(HandleGameStartClick);
        _readyGameBtn.RegisterCallback<ClickEvent>(HandleReadtClick);
    }

    private void Start()
    {
        GameManager.Instance.players.OnListChanged += HandlePlayerListChanged;
        GameManager.Instance.GameStateChanged += HnadleGameStateChanged;

        foreach(GameData data in GameManager.Instance.players)
        {
            HandlePlayerListChanged(new NetworkListEvent<GameData> 
            {
                Type = NetworkListEvent<GameData>.EventType.Add
            });
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.players.OnListChanged -= HandlePlayerListChanged;
        GameManager.Instance.GameStateChanged -= HnadleGameStateChanged;
    }

    private void HnadleGameStateChanged(GameState state)
    {
        if(state == GameState.Game)
        {
            _container.AddToClassList("off");
        }
    }

    private bool CheckPlayerExist(ulong clientID)
    {
        return _players.Any(x => x.clientID == clientID);
    }
    
    private PlayerUI FindEmptyPlayerUI()
    {
        foreach(var playerUI in _players)
        {
            if(playerUI.clientID == 999)
                return playerUI;
        }
        return null;
    }

    private void HandlePlayerListChanged(NetworkListEvent<GameData> evt)
    {
        //Debug.Log($"{evt.Type}, {evt.Value.clientID}");
        switch (evt.Type)
        {
            case NetworkListEvent<GameData>.EventType.Add:
                if (!CheckPlayerExist(evt.Value.clientID))
                {
                    var playerUI = FindEmptyPlayerUI();
                    playerUI.SetGameData(evt.Value);
                    playerUI.SetColor(GameManager.Instance.slimeColors[evt.Value.colorIdx]);
                    playerUI.VisiblePlayerUI();
                }
                break;
            case NetworkListEvent<GameData>.EventType.Remove:
                {
                    PlayerUI playerUI = _players.Find(x => x.clientID == evt.Value.clientID);
                    playerUI.RemovePlayerUI();  
                    break;
                }
            case NetworkListEvent<GameData>.EventType.Value:
                {
                    PlayerUI playerUI = _players.Find(x => x.clientID == evt.Value.clientID);
                    playerUI.SetCheck(evt.Value.ready);
                    break;
                }
        }
    }

    private void HandleReadtClick(ClickEvent evt)
    {
        if(GameManager.Instance.myGameRole != GameRole.Host)
        {
            Debug.Log("���� ȣ��Ʈ�� ���� ������ �����մϴ�.");
            return;
        }

        GameManager.Instance.GameStart();
    }

    private void HandleGameStartClick(ClickEvent evt)
    {
        
    }
}
    
