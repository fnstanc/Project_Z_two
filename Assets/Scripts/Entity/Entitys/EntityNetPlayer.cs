using System;
using System.Collections.Generic;
using UnityEngine;
using Xft;
//using VRTK;

public class EntityNetPlayer : EntityDynamicActor
{
    public override void onStart()
    {
        base.onStart();
        this.CacheObj.layer = 11;
        resetCamera();
    }

    public virtual void resetCamera()
    {

    }

    public override void onDispose()
    {
        base.onDispose();
    }


    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        fsm = new PlayerFSM(this);
        onChangeState(StateType.spawn);
        this.BillBoard.setColorByType(PartType.namePart, Color.green);
    }


    public override void onDamage(DamageData dt)
    {
        if (dt.damage != 0)
        {
            this.HP -= dt.damage;
            Message msg = new Message(MsgCmd.On_Take_Damage, this);
            msg["data"] = dt;
            msg.Send();
        }
        EffectMgr.Instance.createEffect(40003, new EffectInfo(new Vector3(0, 1.6f, 0), this.CacheTrans));
        this.onChangeColor();
        if (this.HP <= 0)
        {
            onChangeState(StateType.die);
        }
        else
        {
            onChangeState(StateType.onHit, new FSMArgs(dt));
        }
    }

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

