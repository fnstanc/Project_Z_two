using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHitState : FSMState
{
    private EntityDynamicActor dyAgent;
    private EntityDynamicActor dyCaster;//攻击方

    private Vector3 orgPos;
    private AttackType atkType = AttackType.normal;
    private float hitDis = 0;
    private Vector3 moveDir = Vector3.zero;
    private string animName;
    private float exitTime;

    public PlayerOnHitState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null)
        {
            orgPos = dyAgent.CacheTrans.position;
            animName = this.getHitAnimName();
            this.exitTime = dyAgent.anim[this.animName].length + Time.timeSinceLevelLoad;
            dyAgent.anim.CrossFade(this.animName, 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Once;
        }
        if (this.args != null && this.args.damageData != null)
        {
            hitDis = this.args.damageData.hitDis;
            atkType = this.args.damageData.atkType;
            if (atkType == AttackType.hitAir)
            {
                dyAgent.setUseGrivaty(false);
                hitDis += orgPos.y;
                moveDir = Vector3.up;
            }
            if (atkType == AttackType.hitBack)
            {
                dyCaster = EntityMgr.Instance.getEntityById(this.args.damageData.casterId) as EntityDynamicActor;
                moveDir = dyCaster.CacheTrans.forward;
            }
        }
    }

    public override void onUpdate()
    {
        if (dyAgent != null)
        {
            doHit();
        }

        //动画播放完毕
        if (Time.timeSinceLevelLoad >= exitTime && dyAgent != null)
        {
            onRelease();
        }
    }

    public override void onExit()
    {
        base.onExit();
        this.dyAgent.setUseGrivaty(true);
    }

    private string getHitAnimName()
    {
        return "hit" + UnityEngine.Random.Range(1, 4);
    }

    private void doHit()
    {
        if (this.atkType == AttackType.normal)
            return;
        if (this.atkType == AttackType.hitAir)
        {
            if (dyAgent.CacheTrans.position.y < hitDis)
                dyAgent.CacheTrans.Translate(moveDir * 0.1f);
        }
        if (this.atkType == AttackType.hitBack)
        {
            if ((orgPos - dyAgent.CacheTrans.position).magnitude < hitDis)
                dyAgent.CacheTrans.Translate(moveDir, Space.World);
        }

    }
}

