using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmorUIManager : MonoBehaviour
{
    private TextMeshProUGUI _tmpCurrentAmmo;
    private TextMeshProUGUI _tmMaxAmmo;

    private void Awake()
    {
        _tmpCurrentAmmo = transform.Find("TxtCurrent").GetComponent<TextMeshProUGUI>();
        _tmMaxAmmo = transform.Find("TXtMax").GetComponent<TextMeshProUGUI>();
    }

    public void SetMaxAmmo(int current, int max)
    {
        _tmMaxAmmo.SetText(current.ToString());
        _tmpCurrentAmmo.SetText(max.ToString());
    }

    public void SetcurrentAmmo(int current)
    {
        _tmpCurrentAmmo.SetText(current.ToString());
    }
}
