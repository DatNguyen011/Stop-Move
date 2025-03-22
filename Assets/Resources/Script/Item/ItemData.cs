using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : Singleton<ItemData>
{
    public List<GameObject> waepons;
    public List<GameObject> hat;
    public List<Material> pants;
    public List<GameObject> shield;
    public List<Bullet> bullets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject GetWeapons(int id)
    {
        return waepons[id];
    }

    public GameObject GetHat(int id)
    {
        return hat[id];
    }
    
    public GameObject GetShield(int id)
    {
        return shield[id];
    }

    public Material GetPants(int id)
    {
        return pants[id];
    }


}
