using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    [SerializeField] private InputReader _reader;

    private void Awake()
    {
        _reader.MovementEvent += HandleMovement;
    }

    private void OnDestroy()
    {
        _reader.MovementEvent -= HandleMovement;    
    }

    private void HandleMovement(Vector2 obj)
    {
        
    }
}
