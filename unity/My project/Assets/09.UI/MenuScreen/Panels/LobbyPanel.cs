using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyPanel
{
    private VisualElement _root;
    private Label _statusLabel;
    private bool _isLobbyRefresh = false;   
    private ScrollView _lobbyView;
    private VisualTreeAsset _lobbyAsset;

    public Label StatusLabel => _statusLabel;
    public event Action<Lobby> JoinLoobbyBtnEvent;

    private bool _isJoining = false;

    public LobbyPanel(VisualElement root, VisualTreeAsset lobbyAsset)
    {
        _root = root;
        _lobbyAsset = lobbyAsset;
        _statusLabel = root.Q<Label>("status-label");
        _lobbyView = root.Q<ScrollView>("lobby-scroll");

        root.Q<Button>("btn-refresh").RegisterCallback<ClickEvent>(HandleRefreshButnClick);
    }

    private void HandleRefreshButnClick(ClickEvent evt)
    {
        Refesh();
    }

    public async void Refesh()
    {
        if (_isLobbyRefresh) return;

        _isLobbyRefresh = true;
        var list = await ApplicationController.Instance.GetLobbyList();
        _lobbyView.Clear();

        foreach (var lobby in list)
        {
            var lobbyTemplate = _lobbyAsset.Instantiate();

            Debug.Log(lobby);
            _lobbyView.Add(lobbyTemplate);
            lobbyTemplate.Q<Label>("lobby-name").text = lobby.Name;
            lobbyTemplate.Q<Button>("btn-join").RegisterCallback<ClickEvent>(evt =>
            {
                //JoinLoobbyBtnEvent?.Invoke(lobbyTemplate);
                //JoinToLobby(lobby);   
            });
        }

        _isLobbyRefresh = false;
    }

   

    public void SetStatusText(string text)
    {
        _statusLabel.text = text;
    }
}
