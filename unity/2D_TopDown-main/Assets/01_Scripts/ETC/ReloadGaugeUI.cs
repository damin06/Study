using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGaugeUI : MonoBehaviour
{
    [SerializeField] private Transform _reloadBar;

    public void ReloadGaugeNormal(float value)
    {
        _reloadBar.transform.localScale = new Vector3 (value, 1f, 1f);
    }
}
