using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : NetworkBehaviour
{
    [Header("참조 값")]
    [SerializeField] private RespawningCoin _coinPrefab;
    [SerializeField] private DeacalCircle _dealCircle;

    [Header("셋팅 값")]
    [SerializeField] private int _maxCoins = 30;
    [SerializeField] private int _coinValue = 10;
    [SerializeField] private float _spawningTerm = 30f;

    private bool _isSpawning = false;
    private float _spawningTime = 0;
    private int _spawnCountTime = 10;

    public List<SpawnPoint> spawnPointList;
    private float _coinRadius;

    public Stack<RespawningCoin> _coinPool = new Stack<RespawningCoin>();   
    private List<RespawningCoin> _activeCoinList = new List<RespawningCoin> ();

    private RespawningCoin SpawnCoin()
    {
        var coin = Instantiate(_coinPrefab, Vector3.zero, Quaternion.identity);
        coin.SetValue(_coinValue);
        coin.GetComponent<NetworkObject>().Spawn();
        coin.OnCollected += HandleCollected;

        return coin;
    }

    private void HandleCollected(RespawningCoin coin)
    {
        _activeCoinList.Remove(coin);
        coin.SetVisible(false);
        coin.isActive.Value = false;
        _coinPool.Push(coin);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        _coinRadius = _coinPrefab.GetComponent<CircleCollider2D>().radius;

        for(int i = 0; i < _maxCoins; i++) 
        {
            var coin = SpawnCoin();
            //coin.isActive.Value = false;
            coin.SetVisible(false);
            _coinPool.Push(coin);
        }
    }

    public override void OnNetworkDespawn()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if(!IsServer) return;   //서버가 아니면 아예 로직이 필요 없다.

        //현재 스포닝이 시작되지 않았고 생성된 코인이 아무것도 없다면 코인 스포닝 타이밍을 재기 시작
        if(!_isSpawning && _activeCoinList.Count == 0)
        {
            _spawningTime += Time.deltaTime;
            if(_spawningTime >= _spawningTerm)
            {
                _spawningTime = 0;
                StartCoroutine(SpawnCoroutine());
            }
        }
    }

    IEnumerator SpawnCoroutine()
    {
        _isSpawning = true;

        int pointIdx = Random.Range(0, spawnPointList.Count);

        var point = spawnPointList[pointIdx];
        int maxCointCoiunt = Mathf.Min(_maxCoins + 1, point.spawnPointList.Count);

        int coinCount = Random.Range(maxCointCoiunt / 2, maxCointCoiunt);

        for (int i = _spawnCountTime; i > 0; i--)
        {
            ServerCountDownMessageClientRpc(i, pointIdx, coinCount);
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < coinCount; i++)
        {
            int end = point.spawnPointList.Count - i - 1;
            int idx = Random.Range(0, end + 1);

            Vector2 pos = point.spawnPointList[idx];

            (point.spawnPointList[idx], point.spawnPointList[end]) = (point.spawnPointList[end], point.spawnPointList[idx]);


            var coin = _coinPool.Pop();
            coin.transform.position = pos;
            coin.Reset();
            _activeCoinList.Add(coin);
            yield return new WaitForSeconds(4f); //4�ʸ��� �Ѱ���
        }
        _isSpawning = false;
        CloseDecalCirecleClientRpc();
    }

    [ClientRpc]
    private void CloseDecalCirecleClientRpc()
    {
        _dealCircle.CloseCircle();
    }

    [ClientRpc]
    private void ServerCountDownMessageClientRpc(int sec, int pointIdx, int coinCount)
    {
        var point = spawnPointList[pointIdx];
        if (!_dealCircle.showDecal)
        {
            _dealCircle.OpenCircle(point.Position, point.Radius);
        }
    }
}
