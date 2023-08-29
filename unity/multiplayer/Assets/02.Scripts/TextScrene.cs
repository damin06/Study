using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

public class TextScrene : MonoBehaviour
{
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _uiDocument.rootVisualElement.Q<Button>("HostBtn")
            .RegisterCallback<ClickEvent>(HandleHostClick);
        _uiDocument.rootVisualElement.Q<Button>("ClientBtn")
            .RegisterCallback<ClickEvent>(HandleClientClick);
    }

    private void HandleHostClick(ClickEvent evt)
    {
        NetworkManager.Singleton.StartHost();
    }

    private void HandleClientClick(ClickEvent evt)
    {
        NetworkManager.Singleton.StartClient();
    }
}
