using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Coin : NetworkBehaviour
{
    protected SpriteRenderer _spriteRenderer;
    protected CircleCollider2D _collider2D; //�̰� �÷��õǸ� �����ؼ� 
    protected int _coinValue = 10; //���� ��
    protected bool _alreadyCollected;

    //�ʱ⿡ ��Ʈ��ũ ����ȭ�� ���� ����
    public NetworkVariable<bool> isActive = new NetworkVariable<bool>();

    public abstract int Collect();

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<CircleCollider2D>();
    }

    public override void OnNetworkSpawn()
    {
        if(IsClient)
        {
            SetVisible(isActive.Value);
        }
    }

    public void SetVisible(bool value)
    {
        _collider2D.enabled = value;
        _spriteRenderer.enabled = value;
    }

    public void SetValue(int value)
    {
        _coinValue = value;
    }
}
