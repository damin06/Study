using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    [SerializeField] private float _lifetime;

    private float _currentTime = 0;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime >= _lifetime)
        {
            Destroy(gameObject);
        }
    }
}
