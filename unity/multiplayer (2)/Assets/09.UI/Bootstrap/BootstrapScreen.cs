using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BootstrapScreen : MonoBehaviour
{
    private UIDocument _uiDocument;
    private TextField _nameTextField;
    private Button _connectBtn;

    public const string PlayerNameKey = "PlayerName";

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        if(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        var root = _uiDocument.rootVisualElement;
        _nameTextField = root.Q<TextField>("name-text-field");
        _nameTextField.RegisterValueChangedCallback<string>(OnNameChangeHandle);
        
        _connectBtn = root.Q<Button>("btn-connect");
        _connectBtn.SetEnabled(false); //처음에 enable을 꺼버린다.
        _connectBtn.RegisterCallback<ClickEvent>(OnConnectHandle);

        string name = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
        ValidateUserName(name); //이게 성공적이면 접속버튼을 풀어주는거고
        _nameTextField.SetValueWithoutNotify(name);
    }

    private void OnNameChangeHandle(ChangeEvent<string> evt)
    {
        ValidateUserName(evt.newValue);
    }

    private void OnConnectHandle(ClickEvent evt)
    {
        //플레이어 Prefs에다가 지금입력한 이름을 다시 저장해주고
        PlayerPrefs.SetString(PlayerNameKey, _nameTextField.text);
        // NetBootstrapScene으로 넘어가 준다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ValidateUserName(string name)
    {
        // @"^[a-zA-Z0-9]{2,8}$"
        //이름을 알파벳 소문자 대문자 숫자만 사용해서 2글자 이상에 8글자 이하로
        Regex regex = new Regex(@"^[a-zA-Z0-9]{2,8}$");
        bool success = regex.IsMatch(name);
        _connectBtn.SetEnabled(success);
    }
}
