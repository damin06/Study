using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    [SerializeField] protected int _playerSortingOrder = 5;
    protected SpriteRenderer _spriterRenderer;

    private void Awake()
    {
        _spriterRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite(bool value)
    {
        Vector3 localScale = new Vector3(1, 1, 1);
        if(value)
        {
            localScale.y = -1;
        }
        transform.localScale = localScale;
    }

    public void RenderBehindHead(bool value)
    {
        _spriterRenderer.sortingOrder = value ? _playerSortingOrder - 1 : _playerSortingOrder + 1;
    }
}
