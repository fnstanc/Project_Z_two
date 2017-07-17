using System;
using System.Collections.Generic;
using UnityEngine;

//只有contorl才能发消息给服务器  UI那边不能这样做(虽然你可以这么做)
public class KnapsackControl : BaseControl
{
    public override void initEnum()
    {
        this.uiEnum = UIEnum.knapsack;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_Knapsack_UI, onOpenUI);
    }

    private void onOpenUI(Message msg)
    {
        bool isOpen = UIMgr.Instance.isOpen(this.uiEnum);
        if (isOpen)
            closeUI();
        else
            openUI();
    }
    private void openUI()
    {
        this.updateUI(new BaseData());
    }
    private void closeUI()
    {
        UIMgr.Instance.closeUI(this.uiEnum);
    }

    //客户端接收到使用物品消息
    private void onUseGoodsMsg(Message msg)
    {
        int tempId = (int)msg["tempId"];
        int count = (int)msg["count"];
        Message netMsg = new Message(MsgCmd.On_Goods_Change, this);
        netMsg["tempId"] = tempId;
        netMsg["count"] = count;
        netMsg.Send();
    }
}

