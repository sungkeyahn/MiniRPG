using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager
{
    GameObject player;
    HashSet<GameObject> monsters = new HashSet<GameObject>();
    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() 
    {
        return player;
    }
    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        UnitController unit = go.GetComponent<UnitController>();
        if (unit == null)
            return Define.WorldObject.Unknown;
        return unit.WorldObjectType;
    }
    public GameObject Spawn(Define.WorldObject type , string path, Transform parent=null)
    {
        GameObject go = Managers.Resourec.Instantiate(path,parent);
        switch (type)
        {
            case Define.WorldObject.Unknown:
                break;
            case Define.WorldObject.Player:
                player = go;
                break;
            case Define.WorldObject.Monster:
                monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
        }
        return go;
    }
    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Unknown:
                break;
            case Define.WorldObject.Player:
                if (player == go)
                    player = null;
                break;
            case Define.WorldObject.Monster:
                if (monsters.Contains(go))
                    if (OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(-1);
                break;
        }
        Managers.Resourec.Destroy(go);
    }
}
