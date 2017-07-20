using System;
using System.Collections.Generic;
using UnityEngine;
using Xft;
//using VRTK;

public class EntityMainPlayer : EntityNetPlayer
{

    public override void onStart()
    {
        base.onStart();
        this.CacheObj.layer = 11;
        resetCamera();
    }

    public override void resetCamera()
    {
        Camera main = Camera.main;
        if (main == null)
        {
            main = new GameObject("MainCamera").AddComponent<Camera>();
            main.tag = "MainCamera";
            MainCameraWidget mcw = main.GetComponent<MainCameraWidget>();
            if (mcw == null)
            {
                mcw = main.gameObject.AddComponent<MainCameraWidget>();
                mcw.setAgent(this);
            }
        }
    }

    public override void onDispose()
    {
        base.onDispose();
        this.BB.removeValueHandler(Attr.target.ToString(), onTargetChange);
    }


    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        this.BB.addValueHandler(Attr.target.ToString(), onTargetChange);

        fsm = new PlayerFSM(this);
        onChangeState(StateType.spawn);
        this.BillBoard.setColorByType(PartType.namePart, Color.green);
    }

    private void onTargetChange(object val)
    {
        Message msg = new Message(MsgCmd.On_MainPlayer_TargetChange, this);
        msg["targetId"] = val;
        msg.Send();
    }

    public override void onDamage(DamageData dt)
    {
        this.HP -= dt.damage;
    }

    #region 玩家移动
    //触摸TouchPad 主角移动
    Quaternion rot = Quaternion.identity;
    Vector3 dir = Vector3.zero;
    public void onPlayerMove(MoveData data)
    {
        dir = data.moveDir;
        dir.z = dir.y;
        dir.y = 0;
        float angle = Vector3.Angle(Vector3.forward, dir);
        angle = dir.x < 0 ? 360 - angle : angle;
        rot = Quaternion.Euler(0, angle, 0);
        this.CacheTrans.rotation = Quaternion.Lerp(this.CacheTrans.rotation, rot, 0.5f);
        CC.Move(dir * moveSpeed);
    }

    public override void onChangeDir(Quaternion rot, float t)
    {
        this.CacheTrans.rotation = Quaternion.Lerp(this.CacheTrans.rotation, rot, t);
    }

    public void onPlayerMoveStart(MoveData data)
    {
        this.onChangeState(StateType.run);
    }
    public void onPlayerMoveEnd(MoveData data)
    {
        this.onChangeState(StateType.idle);
    }
    #endregion

    private XWeaponTrail trail = null;
    public override void activeWeaponTrail(bool isUse = false)
    {
        if (trail == null)
        {
            GameObject weapon = this.getPartObj(EntityPartType.weapon);
            trail = weapon.GetComponent<XWeaponTrail>();
        }

        if (trail != null)
        {
            if (isUse)
                trail.Activate();
            else
                trail.Deactivate();
        }
    }

}

