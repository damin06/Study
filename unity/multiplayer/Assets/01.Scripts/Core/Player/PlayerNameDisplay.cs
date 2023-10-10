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
        //맨처음에 한번은 실행시켜줘라.
        _player.playerName.OnValueChanged += HandlePlayerNameChanged;
    }

    private void HandlePlayerNameChanged(FixedString32Bytes oldName,
                                    FixedString32Bytes newName)
    {
        //이 함수가 실행되면 텍스트를 변경해야 한다.
        _displayText.text = newName.ToString();
    }

    private void OnDestroy()
    {
        // 파괴될때는 안전하게 이벤트를 구독취소 해야한다.
        _player.playerName.OnValueChanged -= HandlePlayerNameChanged;
    }
}
