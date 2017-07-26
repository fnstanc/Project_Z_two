using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnState : FSMState
{
    private float spawnTime = 0;
    private EntityDynamicActor dyAgent;

    public PlayerSpawnState(BaseEntity agent, StateType type) : base(agent, type)
    {
        dyAgent = this.agent as EntityDynamicActor;
    }

    public override void onEnter()
    {
        base.onEnter();
        spawnTime = Time.timeSinceLevelLoad + 1;
        if (dyAgent != null)
        {
            dyAgent.activeWeaponTrail(false);
            dyAgent.anim.CrossFade("idle", 0.1f);
            dyAgent.anim.wrapMode = WrapMode.Loop;
            EffectMgr.Instance.createEffect(10008, new EffectInfo(new Vector3(0, 1.2f, 0), this.dyAgent.CacheTrans));            
        }
    }

    public override void onUpdate()
    {
        if (Time.timeSinceLevelLoad >= spawnTime && dyAgent != null)
        {
            onRelease();
        }
    }

    public override bool isCanChangeTo(StateType type)
    {
        return true;
    }

}

