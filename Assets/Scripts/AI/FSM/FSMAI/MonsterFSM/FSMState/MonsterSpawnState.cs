using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnState : FSMState
{
    private float duration = 1f;

    public MonsterSpawnState(BaseEntity agent, StateType type) : base(agent, type)
    {
    }

    public override void onEnter()
    {
        base.onEnter();
        EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("standUp", 0.1f);
        }
    }

    public override void onUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
            if (dyAgent != null)
            {
                dyAgent.onChangeState(StateType.idle);
            }
        }
    }

}
