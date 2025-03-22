using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviour
{
    public static MyPoolManager InstancePool;
    public Dictionary<GameObject, MyPool> dicPools = new Dictionary<GameObject, MyPool>();
    GameObject tmp;
    private void Awake()
    {
        InstancePool = this;
    }
    public GameObject Get(GameObject obj)
    {
        if (dicPools.ContainsKey(obj) == false)
        {
            dicPools.Add(obj, new MyPool(obj));
        }
        return dicPools[obj].Get();
    }
    public GameObject Get(GameObject obj, Vector3 position)
    {
        tmp = Get(obj);
        tmp.transform.position = position;
        return tmp;
    }
    public T Get<T>(T obj) where T : Component
    {
        tmp = Get(obj.gameObject);
        if (tmp == null) return default;
        return tmp.GetComponent<T>();
    }
    public T Get<T>(T obj, Transform position) where T : Component
    {
        tmp = Get(obj.gameObject);
        if (tmp == null) return default;
        tmp.transform.SetParent(position);
        return tmp.GetComponent<T>();
    }
    public T Get<T>(GameObject obj, Vector3 position) where T : Component
    {
        tmp = Get(obj);
        if (tmp == null) return default;
        tmp.transform.position = position;
        return tmp.GetComponent<T>();
    }
    public void ClearPool<T>(T obj) where T : Component
    {
        GameObject key = obj.gameObject; 

        if (dicPools.ContainsKey(key))
        {
            dicPools[key].Clear(); 
            dicPools.Remove(key);
         
        }


    }

}
