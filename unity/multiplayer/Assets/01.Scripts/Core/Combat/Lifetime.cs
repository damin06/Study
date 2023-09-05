using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
            Destroy(gameObject); //만약 풀링을 할꺼면 클라이언트에서만 풀링할 것. 서버는 풀링 금지.
        }
    }
}
