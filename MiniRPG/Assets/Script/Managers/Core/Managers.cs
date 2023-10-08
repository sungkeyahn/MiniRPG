using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
#region CoreManagers
    static Managers _instance;
    public static Managers Instance { get{ Init();  return _instance; } }

    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    ResourecManager _resourec = new ResourecManager();
    public static ResourecManager Resourec { get { return Instance._resourec; } }

    UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    MySceneManager _scene = new MySceneManager();
    public static MySceneManager Scene { get { return Instance._scene; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }
#endregion

#region ContentsManagers
    MyGameManager game = new MyGameManager();
    public static MyGameManager Game { get { return Instance.game; } }
#endregion

    void Start()
    {
        Init();
    }
    void Update()
    {
        _input.OnUpdate();
    }
    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Scene");
            if (go==null)
            {
                go = new GameObject { name = "@Scene" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();

            _instance._pool.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
