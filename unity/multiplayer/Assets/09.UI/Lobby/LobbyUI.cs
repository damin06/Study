using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class LobbyUI
{
    private VisualTreeAsset _lobbyTemplate;
    private VisualElement _root;
    private VisualElement _popupPanel;
    private ScrollView _lobbyContainer;

    private bool _isJoining;
    private bool _isRefreshing;

    public LobbyUI (VisualTreeAsset lobbyTemplate, VisualElement root, VisualElement popupPanel)
    {
        _lobbyTemplate = lobbyTemplate;
        _root = root;
        _popupPanel = popupPanel;

        _lobbyContainer = _root.Q<ScrollView>("lobby-container");
        _root.Q<Button>("btn-close").RegisterCallback<ClickEvent>(OnCloseHandle);
        _root.Q<Button>("btn-refresh").RegisterCallback<ClickEvent>(e => RefreshList());
    }

    public async void JoinAsync(Lobby lobby)
    {
        if (_isJoining) return;
        _isJoining = true;

        try
        {
            Lobby joiningLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobby.Id);
            string joinCode = joiningLobby.Data["JoinCode"].Value;

            await ClientSingletone.Instance.GameManager.StartClientAsync(joinCode);

        }catch(LobbyServiceException ex)
        {
            Debug.LogError(ex);
        }
        finally
        {
            _isJoining = false;
        }
    }

    private void OnCloseHandle(ClickEvent evt)
    {
        _popupPanel.RemoveFromClassList("on");
    }

    public async void RefreshList()
    {
        if (_isRefreshing) return;
        _isRefreshing = true;

        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count = 25;
            options.Filters = new List<QueryFilter>
            {
                new QueryFilter(field: QueryFilter.FieldOptions.AvailableSlots,
                                op: QueryFilter.OpOptions.GT, value: "0"),
                new QueryFilter(field: QueryFilter.FieldOptions.IsLocked,
                                op: QueryFilter.OpOptions.EQ, value: "0")
            };

            QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);

            _lobbyContainer.Clear(); //현재 그려져있던 모든 로비는 버리고

            foreach(var lobby in lobbies.Results)
            {
                VisualElement template = _lobbyTemplate.Instantiate()
                                            .Q<VisualElement>("lobby-template");
                _lobbyContainer.Add(template);

                LobbyTemplate newLobby = new LobbyTemplate(template, this, lobby);
            }

        }catch(LobbyServiceException ex)
        {
            Debug.LogError(ex);
            throw;
        }

        _isRefreshing = false;
    }

}

