using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuScreen : MonoBehaviour
{
    private UIDocument _uIDocument;
    private VisualElement _contentElem;
    private const string _nameKey = "userName";

    private void Awake()
    {
        _uIDocument = GetComponent<UIDocument>();   
    }

    private void OnEnable()
    {
        var root = _uIDocument.rootVisualElement;
        _contentElem = root.Q<VisualElement>("content");
        root.Q<VisualElement>("popup-panel").RemoveFromClassList("off");

        root.Q<VisualElement>("tap-container").RegisterCallback<ClickEvent>(TapButtonClickHandle);

        var nameText = root.Q<TextField>("name-text");
        nameText.SetValueWithoutNotify(PlayerPrefs.GetString(_nameKey, string.Empty));
        root.Q<Button>("btn-ok").RegisterCallback<ClickEvent>(e =>
        {
            string name = root.Q<TextField>("name-text").value;
            if (string.IsNullOrEmpty(name)) return;

            PlayerPrefs.SetString(_nameKey, name);
            root.Q<VisualElement>("popup-panel").AddToClassList("off");
        });
    }

    private void TapButtonClickHandle(ClickEvent evt)
    {
        if(evt.target is DataVisualElement)
        {
            var dve = evt.target as DataVisualElement;
            var percent = dve.panelIndex * 100;

            _contentElem.style.left = new Length(-percent, LengthUnit.Percent);
            Debug.Log(dve.panelIndex);
        }
    }
}
