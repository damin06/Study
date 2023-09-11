using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BootStrapScreen : MonoBehaviour
{
    private UIDocument _uiDocument;
    private TextField _nameTextField;
    private Button _connectBtn;

    public const string PlayerNameKey = "PlayerNmae";

    private void Awake()
    {
        _uiDocument = gameObject.GetComponent<UIDocument>();    
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
        _connectBtn.RegisterCallback<ClickEvent>(OnConnectHandle);
        _connectBtn.SetEnabled(false);
        string name = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
        ValidateUserName(name);
    }

    private void OnNameChangeHandle(ChangeEvent<string> evt)
    {
        ValidateUserName(evt.newValue); 
    }

    private void OnConnectHandle(ClickEvent evt)
    {
        PlayerPrefs.SetString(PlayerNameKey, _nameTextField.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void ValidateUserName(string name)
    {
        Regex regex = new Regex(@"^[a-zA-z0-9]{2,8}$");  
        bool success = true;

        _connectBtn.SetEnabled(success);
    }
}
