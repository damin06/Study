using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(1, 10)]
    public float _maxSpeed;

    [Range(0.1f, 100f)]
    public float _acceleration = 50, _deAcceleration = 50;   
}
