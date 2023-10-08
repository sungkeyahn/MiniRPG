using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    protected Define.Scene type = Define.Scene.Unknown;
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        
        Object ob = GameObject.FindObjectOfType(typeof(EventSystem));
        if (ob == null)
            Managers.Resourec.Instantiate("UI/EventSystem").name= "@EventSystem";
    }
    public abstract void Clear();
   
}
