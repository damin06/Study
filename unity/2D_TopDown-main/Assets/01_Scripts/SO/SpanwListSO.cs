using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpawnPair
{
    public PoolableMono prefab;
    public float spawnPercent;
}

[CreateAssetMenu(menuName = "SO/SpawnList")]
public class SpanwListSO : ScriptableObject
{
    public List<SpawnPair> _spawnPair;
}
