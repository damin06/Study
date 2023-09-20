using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private TankPlayer _player;
    [SerializeField] private TMP_Text _displayText;

    private void Start()
    {
        HandlePlayerNameChanged(string.Empty, _player.playerName.Value);
        _player.playerName.OnValueChanged += HandlePlayerNameChanged;
    }

    private void HandlePlayerNameChanged(FixedString32Bytes oldName, FixedString32Bytes newName)
    {
        _displayText.text = newName.ToString(); 
    }

    private void OnDestroy()
    {
        _player.playerName.OnValueChanged -= HandlePlayerNameChanged;
    }
}
