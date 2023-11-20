using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _txtusername;

    public void OnConnectedToServer()
    {
        string name = _txtusername.text;
        UserData userData = new UserData()
        {
            username = name
        };

        ClientSingleton.Instance.startClient(userData);
    }
}
