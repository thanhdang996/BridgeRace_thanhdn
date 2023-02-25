using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private int level = 1;
    public int Level { get => level; set => level = value; }


    private GameObject currentMap;
    public GameObject CurrentMap { get => currentMap; set => currentMap = value; }



    private void Start()
    {
        LoadMapAndCreateMap();
    }

    public void RemoveLastMap()
    {
        Destroy(currentMap);
    }

    public void LoadMapAndCreateMap()
    {
        GameObject go = Resources.Load($"Maps/Map {Level}") as GameObject;
        CurrentMap = Instantiate(go);
    }
}
