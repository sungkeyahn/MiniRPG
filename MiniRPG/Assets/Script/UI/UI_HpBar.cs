using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBar : UI_Base
{
    enum GameObjects { HPBar, }

    Stat stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        stat = transform.parent.GetComponent<Stat>();
    }
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = stat.Hp / (float)stat.MaxHP;
        SetHpRatio(ratio);
    }
    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value=ratio;
    }
}
