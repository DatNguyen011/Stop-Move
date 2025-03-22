
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{
    //public NavMeshSurface navMeshSurface;
    public Image imageMaps;
    public List<GameObject> listMaps = new List<GameObject>();
    private int maps;
    //public GameObject defaultMaps;
    public Transform mapParents;
    
    public void getMap()
    {
        //defaultMaps.SetActive(false);
        maps = 1;
        imageMaps.sprite = Resources.Load<Sprite>("UI/Maps/" + maps);
        Instantiate(listMaps[0], mapParents);
        //navMeshSurface.BuildNavMesh();
    }

}
