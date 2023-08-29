using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [Header("참조 변수")]
    [SerializeField] private Transform _barTrm;

    public void HandleHealthCahnged(int oldHealth, int newHealth, float ratio)
    {
        ratio = Mathf.Clamp(ratio, 0, 1);
        _barTrm.localScale = new Vector3(ratio, 0, 0);
    }
}
