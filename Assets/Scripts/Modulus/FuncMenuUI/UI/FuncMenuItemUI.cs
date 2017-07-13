using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuncMenuItemUI : BaseUI
{
    private Image icon;
    private Text itemName;
    private FuncMenuItemData dt;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        icon = this.CacheObj.GetComponent<Image>();
        itemName = this.CacheTrans.Find("itemName").GetComponent<Text>();
        UIEventTrigger listener = this.CacheObj.AddComponent<UIEventTrigger>();
        listener.isShowClickAnim(true);
        listener.setClickHandler(() =>
        {
            FuncMenuItemData dt = this.data as FuncMenuItemData;
            if (dt != null)
            {
                Message msg = new Message(dt.cmdName, this);
                msg.Send();
            }
        });
    }

    public override void refreshUI()
    {
        FuncMenuItemData dt = this.data as FuncMenuItemData;
        if (dt != null)
        {
            icon.sprite = SpriteMgr.Instance.getSprite(dt.icon);
            itemName.text = dt.name;
        }
    }


}

