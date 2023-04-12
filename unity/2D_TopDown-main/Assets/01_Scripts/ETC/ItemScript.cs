using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : PoolableMono
{
    [SerializeField]
    private ResourceDataSO _itemData;
    public ResourceDataSO ItemData => _itemData;

    private AudioSource _audioSoruce;
    private Collider2D _colider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSoruce = GetComponent<AudioSource>();
        _audioSoruce.clip = _itemData.UseSound;
        _colider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpResource()
    {
        StartCoroutine(DestroyCorutine());
    }

    IEnumerator DestroyCorutine()
    {
        _colider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSoruce.Play();
        yield return new WaitForSeconds(_audioSoruce.clip.length + 0.3f);
        PoolManager.Instance.Push(this);
    }
    public override void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Item");
        _spriteRenderer.enabled = true;
        _colider.enabled = true;
    }

}
