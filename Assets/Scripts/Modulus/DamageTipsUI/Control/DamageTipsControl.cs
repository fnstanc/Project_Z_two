using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTipsControl : BaseControl
{
    private DamageTipsData dt = new DamageTipsData();

    public override void initEnum()
    {
        this.uiEnum = UIEnum.damageTipsUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.On_Take_Damage, openUI);
    }

    private void openUI(Message msg)
    {
        DamageData data = msg["data"] as DamageData;
        if (data != null)
        {
            dt.dataQueue.Enqueue(data);
            if (!isOpen())
                this.updateUI(dt);
        }
    }

}
