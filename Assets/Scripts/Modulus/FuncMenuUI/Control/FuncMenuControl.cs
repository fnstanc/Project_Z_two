using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

public class FuncMenuControl : BaseControl
{
    public override void initEnum()
    {
        this.uiEnum = UIEnum.funcMenuUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_FuncMenu_UI, openUI);
    }

    private void openUI(Message msg)
    {
        this.updateUI(initData());
    }

    private FuncMenuData initData()
    {
        FuncMenuData data = new FuncMenuData();

        List<FuncMenuConfigData> lst = GameData.FuncMenuConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            FuncMenuItemData dt = new FuncMenuItemData();
            dt.id = lst[i].id;
            dt.isShow = lst[i].isShow;
            dt.icon = lst[i].icon;
            dt.name = lst[i].name;
            dt.cmdName = lst[i].cmdName;
            if (dt.isShow)
                data.lst.Add(dt);
        }
        return data;
    }

}
