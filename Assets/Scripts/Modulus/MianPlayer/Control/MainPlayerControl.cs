using System;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : BaseControl
{
    private Queue<string> netPlayers = new Queue<string>();

    public override void initEnum()
    {
        this.uiEnum = UIEnum.mainPlayerUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_MainPlayer_UI, onOpenUI);
        //当接受到玩家信息netmsg
        MessageCenter.Instance.addListener(NetCmd.onReqsRoleData.ToString(), createMainPlayer);
        MessageCenter.Instance.addListener(NetCmd.onReqsNetRoleData.ToString(), createNetPlayer);
        //玩家移动
        MessageCenter.Instance.addListener(NetCmd.onReqsSyncPos.ToString(), onSyncRolePos);
        //玩家释放技能
        MessageCenter.Instance.addListener(NetCmd.onReqsRoleCastSkill.ToString(), onSyncRoleSkill);
    }

    private void onOpenUI(Message msg)
    {
        MainPlayerUIData dt = new MainPlayerUIData();
        dt.headIcon = "head1";
        this.updateUI(dt);
    }

    //创建玩家 0是协议号
    private void createMainPlayer(Message msg)
    {
        string netMsg = msg["msg"].ToString();
        string[] lst = netMsg.Split(',');
        int uid = int.Parse(lst[1]);
        int tempid = int.Parse(lst[2]);
        SceneMgr.Instance.onLoadScene("Test", (name) =>
        {
            EntityMgr.Instance.MainPlayerId = uid;
            EntityMgr.Instance.createEntity<EntityMainPlayer>(tempid, uid, (entity) =>
            {
                Message msg1 = new Message(MsgCmd.Open_MainPlayer_UI, this);
                msg1.Send();
                Message msg2 = new Message(MsgCmd.Open_Skill_UI, this);
                msg2.Send();
                Message msg3 = new Message(MsgCmd.Open_JoyStick_UI, this);
                msg3.Send();
                Message msg4 = new Message(MsgCmd.Open_FuncMenu_UI, this);
                msg4.Send();
                onCreateNetPlayer();
            });
        }, true);
    }


    private void createNetPlayer(Message msg)
    {
        string netMsg = msg["msg"].ToString();
        string[] lst = netMsg.Split(';');
        for (int i = 0; i < lst.Length; i++)
        {
            netPlayers.Enqueue(lst[i]);
        }
        if (EntityMgr.Instance.getMainPlayer() != null)
        {
            onCreateNetPlayer();
        }
    }

    private void onCreateNetPlayer()
    {
        while (netPlayers.Count > 0)
        {
            string netMsg = netPlayers.Dequeue();
            string[] lst = netMsg.Split(',');
            int uid = int.Parse(lst[1]);
            int tempid = int.Parse(lst[2]);
            EntityMgr.Instance.createEntity<EntityNetPlayer>(tempid, uid);
        }
    }



    private void onSyncRolePos(Message msg)
    {
        string netMsg = msg["msg"].ToString();
        SyncHelper.onSyncPos(netMsg);
    }
    private void onSyncRoleSkill(Message msg)
    {
        string netMsg = msg["msg"].ToString();
        SyncHelper.onSyncSkill(netMsg);
    }

}
