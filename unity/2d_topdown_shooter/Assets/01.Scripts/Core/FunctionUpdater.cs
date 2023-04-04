using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionUpdater : MonoBehaviour
{
    public static FunctionUpdater Instance;

    private Action _updateAction;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        _updateAction?.Invoke();
    }

    public void Create(Action act)
    {
        _updateAction += act;
    }
}
