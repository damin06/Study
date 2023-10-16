using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bootstrap : MonoBehaviour
{
    private UIDocument _uIDocument;
    private Label _descLabel;

    private void Awake()
    {
        _uIDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uIDocument.rootVisualElement;
        _descLabel = root.Q<Label>("loading-descl");

        ApplicationController.OnMessageEvent += HandleAppMsg;
    }

    private void HandleAppMsg(string msg)
    {
        _descLabel.text = msg;
    }
}
