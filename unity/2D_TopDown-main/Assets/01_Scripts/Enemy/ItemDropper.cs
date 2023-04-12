using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private ItemDorpTable _droTable;
    private float[] _itemWeights;

    [SerializeField] private bool _dorpEffect = false;
    [SerializeField] private float _dropPwoer = 2f;

    [SerializeField][Range(0, 1f)] private float _dropChance;

    private void Start()
    {
        _itemWeights = _droTable.DropList.Select(Item => Item.Rate).ToArray();
    }

    public void DropItem()
    {
        float dropVariable = Random.value;
        if (dropVariable < _dropChance)
        {
            int idx = GetRandomWeightIndex();
            ItemScript item = PoolManager.Instance.Pop(_droTable.DropList[idx].ItemPrefab.name) as ItemScript;

            item.transform.position = transform.position;

            if (_dorpEffect)
            {
                Vector3 offset = Random.insideUnitCircle * 1.5f;
                item.transform.DOJump(transform.position + offset, _dropPwoer, 1, 0.35f);
            }
        }
    }

    private int GetRandomWeightIndex()
    {
        float sum = 0;
        for (int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i];
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for (int i = 0; i < _itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
                return i;
            else
                tempSum += _itemWeights[i];
        }

        return 0;
    }
}
