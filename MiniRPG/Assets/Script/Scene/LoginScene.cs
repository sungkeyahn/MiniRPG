using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoginScene : BaseScene
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    protected override void Init()
    {
        base.Init();
        type = Define.Scene.Login;
    }
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
