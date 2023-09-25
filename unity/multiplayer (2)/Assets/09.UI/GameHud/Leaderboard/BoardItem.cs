using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardItem
{
    private VisualElement _root;
    public VisualElement Root => _root;
    private Label _label;
    private Label _coinLabel;

    private string playerName;
    public ulong ClientID { get; private set; }
    public int Coins { get; private set; }
    public int rank = 1;

    private Color _ownerColor;

    public string Text
    {
        get => _label.text;
        set => _label.text = value;
    }
    public string CoinText
    {
        get => _coinLabel.text;
        set => _coinLabel.text = value;
    }

    public BoardItem(VisualElement root, LeaderboardEntityState state, Color ownerColor)
    {
        _root = root;
        _label = root.Q<Label>("board-label");
        _coinLabel = root.Q<Label>("coin-label");
        playerName = state.playerName.Value;
        ClientID = state.clientID;

        _ownerColor = ownerColor;
        UpdateCoin(Coins);
    }

    public void UpdateCoin(int coins)
    {
        Coins = coins;
        UpdateText();
    }

    public void UpdateText()
    {
        if(ClientID == NetworkManager.Singleton.LocalClientId)  //내꺼 순위를 그리려고 한다.
        {
            _label.style.color = _ownerColor;
        }
        Text = $"{rank}. {playerName}";

        CoinText = $"[{Coins.ToString()}]";
    }

    public void Show(bool value)
    {
        Root.style.visibility = value ? Visibility.Visible : Visibility.Hidden;
    }
}
