using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PollingListSO _poolingList;

    [SerializeField] private Transform _SpawnPointParent;
    [SerializeField] private Transform _playerTrm;
    public Transform PlayerTrm => _playerTrm;


    private List<Transform> _spawnPointList;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeComtroller.Instance = gameObject.AddComponent<TimeComtroller>();

        MakePool();

        _spawnPointList = new List<Transform>();
        _SpawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        _spawnPointList.RemoveAt(0);
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingList.lis.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime;
            //시퀀스를 줘서 한 4~6마리가 스폰포인트 반경 2유닛의 범위에서
            //순차적으로 나오도록 만드는 거지 (이거 다음주까지 한번 만들어봐)
            if (currentTime >= 3f)
            {

                currentTime = 0;
                int idx = UnityEngine.Random.Range(0, _spawnPointList.Count);

                EnemyBrain enemy = PoolManager.Instance.Pop("EnemyGrowler") as EnemyBrain;

                enemy.transform.position = _spawnPointList[idx].position;
                enemy.ShowEnemy();
            }
            yield return null;
        }
    }
}
