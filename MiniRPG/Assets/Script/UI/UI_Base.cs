using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();
    void Start()
    {
        Init();
    }

    protected Dictionary<Type, UnityEngine.Object[]> ob = new Dictionary<Type, UnityEngine.Object[]>();

    //리플렉션 기능을 활용한 함수 
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        ob.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utill.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utill.FindChild<T>(gameObject, names[i], true);
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] obs = null;
        if (ob.TryGetValue(typeof(T), out obs) == false) return null;

        return obs[index] as T;
    }
    //자주사용할 T타입은 제너릭 함수를 상용하는 함수를 따로 하나 더 정의
    //인터페이스를 동일하게 맟춤으로써 사용을 조금더 쉽게
    protected GameObject GetObject(int index) { return Get<GameObject>(index); }
    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }


    public static void BindEvent(GameObject go, Action<PointerEventData>action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Utill.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnPointerClickHandler -= action;
                evt.OnPointerClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            default:
                break;
        }
    }

}
