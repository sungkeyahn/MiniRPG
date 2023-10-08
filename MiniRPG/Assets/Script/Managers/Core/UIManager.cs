using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Utill.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public GameObject Root
    {
        get 
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null) root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    int order = 0;
    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    public T ShowPopupUI<T>(string name = null) where T:UI_Popup
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;
        
        GameObject go = Managers.Resourec.Instantiate($"UI/Popup/{name}");
        T popup = Utill.GetOrAddComponent<T>(go);
        popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0) return;

        UI_Popup popup = popupStack.Pop();
        Managers.Resourec.Destroy(popup.gameObject);
        popup = null;
        order--;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0) return;

        if (popupStack.Peek() != popup) return;
        
        ClosePopupUI();
    }
    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    UI_Scene sceneUI = null;
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.Resourec.Instantiate($"UI/Scene/{name}");
        T scene = Utill.GetOrAddComponent<T>(go);
        sceneUI = scene;

        go.transform.SetParent(Root.transform);

        return scene;
    }

    public T MakeSubUI<T>(Transform parent , string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name)) 
            name = typeof(T).Name;

        GameObject go = Managers.Resourec.Instantiate($"UI/Subitem/{name}");
        
        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>() ;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resourec.Instantiate($"UI/WorldSpace/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utill.GetOrAddComponent<T>(go);
    }

    public void Clear()
    { }
}
