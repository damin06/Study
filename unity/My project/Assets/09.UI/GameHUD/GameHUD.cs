using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class GameHUD : MonoBehaviour
{
    private UIDocument _uiDocument;

    private Button _startGameBtn;
    private Button _readyGameBtn;

    private List<PlayerUI> _players = new();

    public void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();   
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;
        _startGameBtn = root.Q<Button>("btn-start");
        _readyGameBtn = root.Q<Button>("btn-ready");

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
        
    }

    private void HandlePlayerListChanged(NetworkListEvent<GameData> evt)
    {
        Debug.Log($"{evt.Type}, {evt.Value.clientID}");
    }

    private void HandleReadtClick(ClickEvent evt)
    {
        
    }

    private void HandleGameStartClick(ClickEvent evt)
    {
        
    }
}
    
