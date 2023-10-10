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
        _connectBtn.SetEnabled(false); //ó���� enable�� ��������.
        _connectBtn.RegisterCallback<ClickEvent>(OnConnectHandle);

        string name = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
        ValidateUserName(name); //�̰� �������̸� ���ӹ�ư�� Ǯ���ִ°Ű�
        _nameTextField.SetValueWithoutNotify(name);
    }

    private void OnNameChangeHandle(ChangeEvent<string> evt)
    {
        ValidateUserName(evt.newValue);
    }

    private void OnConnectHandle(ClickEvent evt)
    {
        //�÷��̾� Prefs���ٰ� �����Է��� �̸��� �ٽ� �������ְ�
        PlayerPrefs.SetString(PlayerNameKey, _nameTextField.text);
        // NetBootstrapScene���� �Ѿ �ش�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ValidateUserName(string name)
    {
        // @"^[a-zA-Z0-9]{2,8}$"
        //�̸��� ���ĺ� �ҹ��� �빮�� ���ڸ� ����ؼ� 2���� �̻� 8���� ���Ϸ�
        Regex regex = new Regex(@"^[a-zA-Z0-9]{2,8}$");
        bool success = regex.IsMatch(name);
        _connectBtn.SetEnabled(success);
    }
}
