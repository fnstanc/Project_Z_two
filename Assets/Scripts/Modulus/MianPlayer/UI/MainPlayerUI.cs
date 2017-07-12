using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayerUI : BaseUI
{

    private Image headIcon;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.mainPlayerUI;
        this.uiNode = UINode.root;
    }

    public override void onStart()
    {
        headIcon = this.CacheTrans.Find("HeadPanel/headIcon").GetComponent<Image>();
    }


    public override void refreshUI()
    {
        MainPlayerUIData dt = this.data as MainPlayerUIData;
        if (dt != null)
        {
            this.headIcon.sprite = SpriteMgr.Instance.getSprite(dt.headIcon);
        }
    }

}

