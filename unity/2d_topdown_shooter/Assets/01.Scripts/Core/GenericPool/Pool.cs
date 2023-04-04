using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> _pool = new Stack<T>();
    private T _prefab; //�̰� ���ڶ� �� �� �뵵�� �ϳ� ������ �־�� �ϰ�
    private Transform _parent; //������ų �θ� �ϳ� ������ �־�� �ϰ�
    
    public Pool(T prefab, Transform parent, int count)
    {
        _prefab = prefab;
        _parent = parent;

        for(int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            //Ŭ���̶�� �̸��� ��������
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;

        if(_pool.Count <= 0)  //���ÿ� ���� Ǯ���صа� �� ��������
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop(); //���ÿ� �������߿� ���� ���� ���� ������
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
