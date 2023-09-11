using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _lobbyTemplate;
    private TextField _textIpAdress;
    private TextField _textPortAdress;
    private TextField _textJoinCode;

    private UIDocument _uiDocument;

    private const string GameSceneName = "Game";
    private VisualElement _popupPanel;
    private LobbyUI _lobbyUI;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();   
    }

    private void OnEnable()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
        var root = _uiDocument.rootVisualElement;
        _textIpAdress = root.Q<TextField>("txt-ip-address");
        _textPortAdress = root.Q<TextField>("txt-port");
        _textJoinCode = root.Q<TextField>("txt-joincode");

        _popupPanel = root.Q<VisualElement>("popup-panel");
        var lobbyRoot = _popupPanel.Q<VisualElement>("lobby-frame");
        _lobbyUI = new LobbyUI(_lobbyTemplate, lobbyRoot, _popupPanel);

        root.Q<Button>("btn-local-host").RegisterCallback<ClickEvent>(OnHandleLocalHost);
        root.Q<Button>("btn-local-client").RegisterCallback<ClickEvent>(OnHandleLocalClient); 
        root.Q<Button>("btn-relay-host").RegisterCallback<ClickEvent>(OnHandleRelayHost);
        root.Q<Button>("btn-joincode").RegisterCallback<ClickEvent>(OnHandleRelayJoin);
        root.Q<Button>("btn-lobby").RegisterCallback<ClickEvent>(OnHandleLobbyOpen);
    }

    private void OnHandleLobbyOpen(ClickEvent evt)
    {
        _popupPanel.AddToClassList("on");

        _lobbyUI.RefreshList();

    }

    private async void OnHandleRelayJoin(ClickEvent evt)
    {
        string code = _textJoinCode.value;
        await ClientSingletone.Instance.GameManager.StartClientAsync(code);
    }

    private async void OnHandleRelayHost(ClickEvent evt)
    {
        await HostSingletone.Instance.GameManager.StartHostAsync();
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton == null) return;
        NetworkManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
    }

    private void HandleClientDisconnected(ulong obj)
    {
        Debug.Log(obj + ", 에러발생");
    }

    private void OnHandleLocalHost(ClickEvent evt)
    {
        if (!SetUpNetworkPassport()) return;
        if (NetworkManager.Singleton.StartHost())
        {
            NetworkManager.Singleton.SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
        }
        else
        {
            NetworkManager.Singleton.Shutdown();
        }
    }

    private bool SetUpNetworkPassport()
    {
        var ip = _textIpAdress.value;
        var port = _textPortAdress.value;

        var portRegex = new Regex(@"^[0-9]{3,5}$");
        var ipRegEx = new Regex(@"^[0-9\.]+$");

        var ipMatch = ipRegEx.Match(ip);    
        var portMatch = portRegex.Match(port);

        if(!portMatch.Success || !ipMatch.Success)
        {
            Debug.LogError("올바르지 못한 아이피 또는 포트 번호입니다.");
            return false;
        }

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            ip,
            (ushort)int.Parse(port)
            );
        return true;
    }

    private void OnHandleLocalClient(ClickEvent evt)
    {
        if (!SetUpNetworkPassport()) return;
        if (!NetworkManager.Singleton.StartClient())
        {
            NetworkManager.Singleton.Shutdown();
        }
    }
}
