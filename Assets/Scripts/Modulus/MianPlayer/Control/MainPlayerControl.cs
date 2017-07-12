using System;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : BaseControl
{


    public override void initEnum()
    {
        this.uiEnum = UIEnum.mainPlayerUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_MainPlayer_UI, onOpenUI);
  
    }

    private void onOpenUI(Message msg)
    {
        MainPlayerUIData dt = new MainPlayerUIData();
        dt.headIcon = "head1";
        this.updateUI(dt);
    }


}
