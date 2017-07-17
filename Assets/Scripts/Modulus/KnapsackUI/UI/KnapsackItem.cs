using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class KnapsackItem : BaseUI
{
    private Image frame;
    private Image icon;
    private Text count;
    private Text type;
    private Text sonType;
    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.none;
        this.uiNode = UINode.none;
    }

    public override void onStart()
    {
        base.onStart();
        frame = this.CacheTrans.Find("frame").GetComponent<Image>();
        icon = this.CacheTrans.Find("icon").GetComponent<Image>();
        count = this.CacheTrans.Find("count").GetComponent<Text>();
        type = this.CacheTrans.Find("type").GetComponent<Text>();
        sonType = this.CacheTrans.Find("sonType").GetComponent<Text>();
        UIEventTrigger listener = this.CacheObj.AddComponent<UIEventTrigger>();
        listener.setEnterHandler(onRayEnter);
        listener.setClickHandler(onRayClick);
        listener.setExitHandler(onRayExit);
    }

    public override void refreshUI()
    {
        base.refreshUI();
        KnapsackItemData dt = this.data as KnapsackItemData;
        if (dt != null)
        {
            count.text = dt.Count + "";
            type.text = dt.Type + "";
            sonType.text = dt.SonType + "";
        }
    }

    private void onRayEnter()
    {
        KnapsackItemData dt = this.data as KnapsackItemData;
        if (dt != null)
        {
            dt.Pos = this.CacheTrans.position;
        }
        UIMgr.Instance.openUI(UIEnum.knapsackTips, this.data);
    }
    private void onRayExit()
    {
        UIMgr.Instance.closeUI(UIEnum.knapsackTips);
    }
    private void onRayClick()
    {
        KnapsackItemData dt = this.data as KnapsackItemData;
        Message msg = new Message(MsgCmd.Client_Use_Goods, this);
        msg["tempId"] = dt.TempId;
        msg["count"] = 1;
        msg.Send();
    }
}

