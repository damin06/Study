using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyTemplate
{
    private VisualElement _root;
    private Label _lobbyNameLabel;

    public string LobbyName
    {
        get => _lobbyNameLabel.text;
        set => _lobbyNameLabel.text = value;    
    }

    private Label _lobbyPlayerLabel; //몇명
    public string LobbyPlayer
    {
        get => _lobbyPlayerLabel.text;
        set => _lobbyPlayerLabel.text = value;
    }

    private Button _joinBtn;
    private Lobby _lobby; //이 로비에 대한 모델정보도 같이 들고 있어야 한다.
    private LobbyUI _lobbyUI;//이게 메인로비

    public LobbyTemplate(VisualElement root, LobbyUI lobbyUI, Lobby lobby)
    {
        _root = root;
        _lobby = lobby;
        _lobbyUI = lobbyUI;

        _lobbyNameLabel = root.Q<Label>("room-name");
        _lobbyPlayerLabel = root.Q<Label>("room-count");
        _joinBtn = root.Q<Button>("btn-join-lobby");
        _joinBtn.RegisterCallback<ClickEvent>(OnJoinHandle);

        LobbyName = lobby.Name;
        LobbyPlayer = $"{lobby.Players.Count} / {lobby.MaxPlayers}";
    }

    private void OnJoinHandle(ClickEvent evt)
    {
        _lobbyUI.JoinAsync(_lobby);
    }
}
