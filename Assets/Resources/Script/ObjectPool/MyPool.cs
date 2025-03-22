using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPool
{
    private Stack<GameObject> stack = new Stack<GameObject>();
    private GameObject baseObj;
    private GameObject tmp;
    private ReturnToMyPool returnPool;

    public MyPool(GameObject baseObj)
    {
        this.baseObj = baseObj;
    }

    public GameObject Get()
    {
        while (stack.Count > 0)
        {
            tmp = stack.Pop();
            if (tmp != null)
            {
                tmp.SetActive(true);
                return tmp;
            }
            else
            {
                Debug.LogWarning($"game object with key {baseObj.name} has been destroyed!");
            }
        }
        tmp = GameObject.Instantiate(baseObj);
        returnPool = tmp.AddComponent<ReturnToMyPool>();
        returnPool.pool = this;
        return tmp;
    }

    public void AddToPool(GameObject obj)
    {
        stack.Push(obj);
    }

    public void Clear()
    {
        while (stack.Count > 0)
        {
            GameObject obj = stack.Pop();
            if (obj != null)
            {
                GameObject.Destroy(obj);
            }
        }
        stack.Clear();
    }
}

