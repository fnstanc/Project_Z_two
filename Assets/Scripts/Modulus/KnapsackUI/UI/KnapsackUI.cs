using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KnapsackUI : BaseUI
{


    public override void resetUIInfo()
    {
        uiEnum = UIEnum.knapsack;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {

        UIUtils.addCommonBg(this);
    }


    public override void refreshUI()
    {

    }

    public override void onActive()
    {
        base.onActive();
        this.CacheTrans.localScale = new Vector3(0, 0, 0);
        this.CacheTrans.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.InOutBack);
    }

    public override void onDeActive()
    {
        base.onDeActive();
        this.CacheTrans.DOScale(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear);
    }

}
