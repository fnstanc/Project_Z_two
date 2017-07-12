using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerYaSuoRState : FSMState
{
    private EntityDynamicActor dyAgent;

    private bool isArrive = false;
    private Vector3 moveDir = Vector3.zero;

    private float exitTime = 0;

    public PlayerYaSuoRState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        if (dyAgent != null && dyAgent.Target != null)
        {
            EntityDynamicActor target = dyAgent.Target as EntityDynamicActor;
            if (target != null)
            {
                target.setUseGrivaty(false);
            }
            dyAgent.setUseGrivaty(false);
        }

    }

    public override void onUpdate()
    {
        if (!isArrive)
        {
            moveDir = (dyAgent.Target.CacheTrans.position - dyAgent.CacheTrans.position);
            if (moveDir.magnitude <= 2f)
            {
                exitTime = Time.timeSinceLevelLoad + 1f;
                dyAgent.anim.CrossFade("skill1", 0.1f);
                EffectMgr.Instance.createEffect(10001, new EffectInfo(new Vector3(0, 1.2f, 0), this.dyAgent.CacheTrans));
                AudioMgr.Instance.playAudioAtPoint(10007, dyAgent.CacheTrans.position);
                isArrive = true;
            }
            dyAgent.CacheTrans.Translate(moveDir.normalized * 0.5f);
        }
        if (isArrive && Time.timeSinceLevelLoad >= exitTime)
        {
            onRelease();
        }

    }

    public override void onExit()
    {
        base.onExit();
        isArrive = false;
        if (dyAgent != null && dyAgent.Target != null)
        {
            dyAgent.setUseGrivaty(true);
            EntityDynamicActor target = dyAgent.Target as EntityDynamicActor;
            if (target != null)
            {
                target.setUseGrivaty(true);
            }
        }
    }

}
