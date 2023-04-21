using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Create Info")]
    public List<Transform> _points = new List<Transform>();
    public List<GameObject> _monsterPool = new List<GameObject>();
    public GameObject _monster;
    public float _createTime = 2f;
    public int _maxMonster = 10;

    void Start()
    {
        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        foreach (Transform point in spawnPointGroup)
        {
            _points.Add(point);
        }

        InvokeRepeating("CreateMonster", 2f, _createTime);

        HideCursor(true);

        CreateMonsterPool();
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
        for (int i = 0; i < _maxMonster; i++)
        {
            var monster = Instantiate<GameObject>(_monster);

            monster.name = $"Monster_{i:00}";

            monster.SetActive(false);

            _monsterPool.Add(monster);
        }
    }

    public void CreateMonster()
    {
        int idx = Random.Range(0, _points.Count);

        GameObject _monster = GetMonsterPool();

        _monster?.transform.SetPositionAndRotation(_points[idx].position, _points[idx].rotation);

        _monster?.SetActive(true);
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
}
