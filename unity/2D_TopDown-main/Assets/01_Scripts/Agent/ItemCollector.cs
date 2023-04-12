using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private int _magneticRange, _magneticPower;

    private int _resourceLayer;
    [SerializeField] private LayerMask _lay;

    private List<ItemScript> _collectList = new List<ItemScript>();

    private void FixedUpdate()
    {
        Collider2D[] resources = Physics2D.OverlapCircleAll(transform.position, _magneticRange, _lay);

        var a = _collectList;

        foreach (Collider2D r in resources)
        {
            if (r.TryGetComponent<ItemScript>(out ItemScript item))
            {
                _collectList.Add(item);
                item.gameObject.layer = 0;
            }
        }

        for (int i = 0; i < _collectList.Count; i++)
        {
            ItemScript item = _collectList[i];
            Vector3 dir = (transform.position - item.transform.position).normalized;
            item.transform.Translate(dir * _magneticPower * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, item.transform.position) < 0.1f)
            {
                int value = item.ItemData.GetAmount();

                PopupText text = PoolManager.Instance.Pop("PopUpText") as PopupText;
                text.SetUp(value.ToString(), transform.position + new Vector3(0, 0.5f, 0),
                item.ItemData.PopupTextColor);

                ProcessItem(item.ItemData.itemType, value);
                item.PickUpResource();
                _collectList.RemoveAt(i);
                i--;
            }
        }
    }

    public UnityEvent<int> OnAmooAdded = null;

    private void ProcessItem(ItemType type, int value)
    {
        switch (type)
        {
            case ItemType.Ammo:
                OnAmooAdded?.Invoke(value);
                break;
        }
    }
    // private void Awake()
    // {
    //     _resourceLayer = LayerMask.NameToLayer("Item");
    //   1<< _resourceLayer
    //다른 방법
    // }

    private void WaitForFixedUpdate()
    {
        Collider2D[] resouces = Physics2D.OverlapCircleAll(transform.position, _magneticRange, 1 << _lay);
    }

    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _magneticRange);
            Gizmos.color = Color.white;
        }
    }
}
