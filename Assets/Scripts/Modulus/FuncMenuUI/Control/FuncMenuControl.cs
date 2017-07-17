using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        FuncMenuConfigConfig[] lst = FuncMenuConfigConfig.GetValues();
        for (int i = 0; i < lst.Length; i++)
        {
            FuncMenuItemData dt = new FuncMenuItemData();
            dt.id = lst[i].tempId;
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
