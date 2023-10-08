using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects { ItemIcon,ItemNameText, }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text="asd";
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { });
    }

    string iteminfo;
    public void SetInfo(string info)
    { iteminfo = info; }

}
