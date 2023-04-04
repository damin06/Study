using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _playerTrm; //이건 나중에 찾아오는 형식으로 변경해야 함.
    public Transform PlayerTrm => _playerTrm;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeController.Instance = transform.AddComponent<TimeController>();

        MakePool();
    }


    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform); //풀매니저 만들어주고
        _poolingList.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));  
        
    }


}
