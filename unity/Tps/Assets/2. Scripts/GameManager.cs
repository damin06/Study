using System.Drawing;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [Header("Enemy Create Info")]
    public List<Transform> _points = new List<Transform>();
    public List<GameObject> _monsterPool = new List<GameObject>();
    public GameObject _monster;
    public float _createTime = 2f;
    public int _maxMonster = 10;

    [SerializeField] private PoolListSO initList;

    private void Awake()
    {
        CreateMonsterPool();
    }

    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        foreach (Transform point in spawnPointGroup)
        {
            _points.Add(point);
        }

        InvokeRepeating("CreateMonster", 2f, _createTime);

        HideCursor(true);

        CreateMonster();
    }

    void HideCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !state;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCursor(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            HideCursor(true);
        }
    }

    public void CreateMonsterPool()
    {
        // for (int i = 0; i < _maxMonster; i++)
        // {
        //     var monster = Instantiate<GameObject>(_monster);

        //     monster.name = $"Monster_{i:00}";

        //     monster.SetActive(false);

        //     _monsterPool.Add(monster);
        // }

        PoolManager.Instance = new PoolManager(transform);
        initList.Pairs.ForEach(p => PoolManager.Instance.CreatePool(p.Prefab, p.count));
    }

    public void CreateMonster()
    {
        // int idx = Random.Range(0, _points.Count);

        // GameObject _monster = GetMonsterPool();

        // _monster?.transform.SetPositionAndRotation(_points[idx].position, _points[idx].rotation);

        // _monster?.SetActive(true);
        MonsterController m = PoolManager.Instance.Pop("Monster") as MonsterController;

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;
        foreach (Transform poin in spawnPointGroup)
        {
            _points.Add(poin);
        }
        int idx = Random.Range(0, _points.Count);
        m?.transform.SetPositionAndRotation(_points[idx].position, _points[idx].rotation);
    }

    private GameObject GetMonsterPool()
    {
        foreach (var _monster in _monsterPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;
            }
        }

        return null;
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        initList.Pairs.ForEach(p =>
        {
            PoolManager.Instance.CreatePool(p.Prefab, p.count);
        });
    }
}
