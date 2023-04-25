using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingPair
{
    public PoolableMono Prefab;
    public int count;
}

[CreateAssetMenu(menuName = "SO/PoolList")]
public class PoolListSO : ScriptableObject
{
    public List<PoolingPair> Pairs;
}
