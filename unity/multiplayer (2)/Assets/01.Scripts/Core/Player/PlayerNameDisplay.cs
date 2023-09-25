using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class PlayerNameDisplay : MonoBehaviour
{
    [SerializeField] private TankPlayer _player;
    [SerializeField] private TMP_Text _displayText;

    private void Start()
    {
        HandlePlayerNameChanged(string.Empty, _player.playerName.Value);
        //��ó���� �ѹ��� ����������.
        _player.playerName.OnValueChanged += HandlePlayerNameChanged;
    }

    private void HandlePlayerNameChanged(FixedString32Bytes oldName,
                                    FixedString32Bytes newName)
    {
        //�� �Լ��� ����Ǹ� �ؽ�Ʈ�� �����ؾ� �Ѵ�.
        _displayText.text = newName.ToString();
    }

    private void OnDestroy()
    {
        // �ı��ɶ��� �����ϰ� �̺�Ʈ�� ������� �ؾ��Ѵ�.
        _player.playerName.OnValueChanged -= HandlePlayerNameChanged;
    }
}
