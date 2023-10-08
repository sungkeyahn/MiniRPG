using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utill
{
    //유틸성함수들 모음

    //자신의 자식을 순차적으로 스캔하면서 이름에 맞는 자식이 있는지 확인해주고 해당 자식의 컴포넌트를 반환해 주는 함수
    public static T FindChild<T>(GameObject go,string name=null,bool recursive=false) where T : UnityEngine.Object 
    {
        if (go == null) return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform tr = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T component = tr.GetComponent<T>();
                    if (component != null) return component;
                }    
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
    //GameObject를 반환해주는 버전
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform tr= FindChild<Transform>(go, name, recursive);
        if (tr == null) return null;
        return tr.gameObject;
    }

    //컴포넌트 추가를 코드상에서 진행하고 싶을때 사용하는 유틸함수
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T com = go.GetComponent<T>();
        if (com == null)
            com = go.AddComponent<T>();
        return com;
    }
}
