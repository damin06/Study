using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; //이건 모자랄 때 찍어낼 용도로 하나 가지고 있어야 하고
    private Transform _parent; //생성시킬 부모도 하나 가지고 있어야 하고
    
    public Pool(T prefab, Transform parent, int count)
    {
        _prefab = prefab;
        _parent = parent;

        for(int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            //클론이라는 이름을 지워버려
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;

        if(_pool.Count <= 0)  //스택에 현재 풀링해둔게 다 떨어졌어
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop(); //스택에 남은거중에 가장 위에 꺼를 가져와
            obj.gameObject.SetActive(true);
        }

        return obj;
    }
    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}
