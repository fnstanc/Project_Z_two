using System;
using System.Collections.Generic;
using UnityEngine;
using ChuMeng;

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

    //根据模板id获取物品config
    private ItemConfigData getGoodsConfig(int tempId)
    {
        ItemConfigData config = null;
        List<ItemConfigData> lst = GameData.ItemConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].tempId == tempId)
            {
                config = lst[i];
            }
        }
        return config;
    }

    //弃用 应该使用服务器与客户端静态数据结合的数据
    private KnapsackData getData()
    {
        KnapsackData data = new KnapsackData();
        List<ItemConfigData> lst = GameData.ItemConfig;
        for (int i = 0; i < lst.Count; i++)
        {
            KnapsackItemData dt = new KnapsackItemData();
            dt.TempId = lst[i].tempId;
            dt.Name = lst[i].name;
            dt.Type = lst[i].type;
            dt.SonType = lst[i].sonType;
            dt.Count = lst[i].count;
            dt.Desc = lst[i].desc;
            //data.lst.Add(dt);
        }
        return data;
    }


}

