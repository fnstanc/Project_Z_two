using System;
using System.Collections.Generic;
using ChuMeng;
using UnityEngine;

public class MainMeunControl : BaseControl
{
    private List<MainMeunItemData> infoLst = null;

    public override void initEnum()
    {
        this.uiEnum = UIEnum.mainMeun;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_Main_Meun_UI, onOpenUI);
    }

    private void onOpenUI(Message msg)
    {
        this.updateUI(new BaseData());
    }
}

