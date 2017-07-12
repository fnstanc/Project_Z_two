using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHitState : FSMState
{
    private EntityDynamicActor dyAgent;

    private float orgHeight;
    private float airHieght = 1.2f;
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
            orgHeight = dyAgent.CacheTrans.position.y;
            animName = this.getHitAnimName();
            this.exitTime = dyAgent.anim[this.animName].length + Time.timeSinceLevelLoad;
            dyAgent.anim.CrossFade(this.animName, 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Once;
            dyAgent.setUseGrivaty(false);
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

    }

    private string getHitAnimName()
    {
        return "hit" + UnityEngine.Random.Range(1, 4);
    }

    private void doHit()
    {
        orgHeight = dyAgent.CacheTrans.position.y;
        if (orgHeight < airHieght)
            dyAgent.CacheTrans.Translate(Vector3.up * 0.1f);
    }
}

