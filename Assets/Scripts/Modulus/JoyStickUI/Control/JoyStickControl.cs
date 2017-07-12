using System;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickControl : BaseControl
{
    private EntityMainPlayer player;

    public override void initEnum()
    {
        this.uiEnum = UIEnum.joyStickUI;
    }

    public override void initListener()
    {
        MessageCenter.Instance.addListener(MsgCmd.Open_JoyStick_UI, onUIOpen);
        MessageCenter.Instance.addListener(MsgCmd.On_MainPlayer_Moving, onPlayerMoving);
        MessageCenter.Instance.addListener(MsgCmd.On_MainPlayer_Move_Start, onPlayerMovingStart);
        MessageCenter.Instance.addListener(MsgCmd.On_MainPlayer_Move_End, onPlayerMovingEnd);
    }

    public void onUIOpen(Message msg)
    {
        this.updateUI(null);
    }
    //玩家移动
    private void onPlayerMoving(Message msg)
    {
        Vector3 dir = (Vector3)msg["dir"];
        MoveData dt = new MoveData();
        dt.moveDir = dir;
        if (player == null)
        {
            player = EntityMgr.Instance.getMainPlayer() as EntityMainPlayer;
        }
        if (player != null)
        {
            player.onPlayerMove(dt);
        }
    }
    //玩家开始移动
    private void onPlayerMovingStart(Message msg)
    {
        if (player == null)
        {
            player = EntityMgr.Instance.getMainPlayer() as EntityMainPlayer;
        }
        if (player != null)
        {
            player.onPlayerMoveStart(null);
        }
    }
    //玩家停止移动
    private void onPlayerMovingEnd(Message msg)
    {
        if (player == null)
        {
            player = EntityMgr.Instance.getMainPlayer() as EntityMainPlayer;
        }
        if (player != null)
        {
            player.onPlayerMoveEnd(null);
        }
    }
}

