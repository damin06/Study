using System.Collections;
using System.Collections.Generic;
using UnityEngine

public class KnifeBase : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2;
    private float _currentLisfeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
