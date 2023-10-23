using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI
{
    private VisualElement _sprite;
    private Label _nameLabel;
    private VisualElement _root;

    public ulong clientID;

    public PlayerUI(VisualElement root, GameData data)
    {
        _root = root;
        _sprite = root.Q<VisualElement>("sprite");
        _nameLabel = root.Q<Label>("name-label");
        clientID = data.clientID;
        _nameLabel.text = data.playerName.Value.ToString();
    }

    public void SetGameData(GameData data) 
    {
        clientID = data.clientID;
        _nameLabel.text = data.playerName.Value;
    }

    public void SetCheck(bool check)
    {
        if(check)
        {
            _root.AddToClassList("ready");
        }
        else
        {
            _root.RemoveFromClassList("ready");
        }
    }

    public void SetColor(Color color)
    {
        _sprite.style.unityBackgroundImageTintColor = color;    
    }

    public void RemovePlayerUI()
    {
        _root.style.visibility = Visibility.Hidden;
    }
}
